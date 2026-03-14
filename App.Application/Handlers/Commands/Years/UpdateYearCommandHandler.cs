using App.Application.Commands.Years;
using App.Application.Contracts.Responses.Years;
using App.Application.Errors;

namespace App.Application.Handlers.Commands.Years;

public class UpdateYearCommandHandler(IUnitOfWork unitOfWork
    ,YearErrors yearErrors) : IRequestHandler<UpdateYearCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly YearErrors _yearErrors = yearErrors;

    public async Task<Result> Handle(UpdateYearCommand request, CancellationToken cancellationToken)
    {

        var year = await _unitOfWork.AcademicYears.FindAsync(x => x.Id==request.Id);

        if (year==null)
            return Result.Failure(_yearErrors.NotFound);


        var isTermExists = await _unitOfWork.Terms.IsExistAsync(x => x.Id == request.ActiveTermId);

        if (!isTermExists)
            return Result.Failure(_yearErrors.TermNotFound);

        if (await _unitOfWork.AcademicYears.IsExistAsync(x => x.FacultyId == year.FacultyId && x.Name == request.Name && x.Id != request.Id))
            return Result.Failure(_yearErrors.DuplicatedYear); 

        year.Name= request.Name;
        year.StartDate=request.StartDate;
        year.EndDate= request.EndDate;

        _unitOfWork.AcademicYears.Update(year);

        var oldYearTerm = await _unitOfWork.YearTerms.FindAsync(x=>x.IsActive==true && x.YearId==year.Id);

        if (oldYearTerm is null)
            return Result.Failure(_yearErrors.TermNotFound);

        oldYearTerm.IsActive = false;

        _unitOfWork.YearTerms.Update(oldYearTerm);

        var newYearTerm = await _unitOfWork.YearTerms.FindAsync(x => x.TermId==request.ActiveTermId && x.YearId == year.Id);

        if (newYearTerm is null)
            return Result.Failure(_yearErrors.TermNotFound);

        newYearTerm.IsActive= true;

        _unitOfWork.YearTerms.Update(newYearTerm);


        await _unitOfWork.SaveAsync();

        return Result.Success();
    }
}
