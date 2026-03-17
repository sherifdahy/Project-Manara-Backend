using App.Application.Commands.Departments;
using App.Application.Commands.Years;

namespace App.Application.Handlers.Commands.Years;

public class ToggleStatusYearCommandHandler(IUnitOfWork unitOfWork,YearErrors yearErrors) : IRequestHandler<ToggleStatusYearCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly YearErrors _yearErrors = yearErrors;


    public async Task<Result> Handle(ToggleStatusYearCommand request, CancellationToken cancellationToken)
    {
        var year = await _unitOfWork.AcademicYears.GetByIdAsync(request.Id);

        if (year is null)
            return Result.Failure(_yearErrors.NotFound);

        year.IsDeleted = !year.IsDeleted;

        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();
    }
}

