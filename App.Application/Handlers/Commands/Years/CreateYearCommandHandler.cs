using App.Application.Commands.Years;
using App.Application.Contracts.Responses.Departments;
using App.Application.Contracts.Responses.Years;
using App.Core.Entities.Academic;
using App.Core.Entities.Relations;

namespace App.Application.Handlers.Commands.Years;

public class CreateYearCommandHandler(IUnitOfWork unitOfWork
    ,FacultyErrors facultyErrors
    ,YearErrors yearErrors) : IRequestHandler<CreateYearCommand, Result<YearResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly FacultyErrors _facultyErrors = facultyErrors;
    private readonly YearErrors _yearErrors = yearErrors;


    public async Task<Result<YearResponse>> Handle(CreateYearCommand request, CancellationToken cancellationToken)
    {

        var isFacultyExists = await _unitOfWork.Fauclties.IsExistAsync(f => f.Id == request.FacultyId);

        if (!isFacultyExists)
            return Result.Failure<YearResponse>(_facultyErrors.NotFound);


        var isTermExists = await _unitOfWork.Terms.IsExistAsync(x=>x.Id==request.ActiveTermId);

        if (!isTermExists)
            return Result.Failure<YearResponse>(_yearErrors.TermNotFound);


        var isYearExists = await _unitOfWork.AcademicYears.IsExistAsync(x=>x.FacultyId==request.FacultyId && x.Name==request.Name);

        if (isYearExists)
            return Result.Failure<YearResponse>(_yearErrors.DuplicatedYear);

        var year = request.Adapt<AcademicYear>();

        var terms = await _unitOfWork.Terms.GetAllAsync();

        foreach(var term in terms)
        {
            if (term.Id == request.ActiveTermId)
                year.YearTerms.Add(new YearTerm() { TermId = term.Id, IsActive = true });
            else
                year.YearTerms.Add(new YearTerm() { TermId = term.Id, IsActive = false });
        }

        await _unitOfWork.AcademicYears.AddAsync(year);

        await _unitOfWork.SaveAsync();

        var response = year.Adapt<YearResponse>();

        return Result.Success(response);
    }
}