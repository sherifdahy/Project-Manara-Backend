using App.Application.Commands.Faculties;

namespace App.Application.Handlers.Commands.Faculties;

public class ToggleStatusFacultyCommandHandler(IUnitOfWork unitOfWork,FacultyErrors errors) : IRequestHandler<ToggleStatusFacultyCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly FacultyErrors _errors = errors;

    public async Task<Result> Handle(ToggleStatusFacultyCommand request, CancellationToken cancellationToken)
    {

        var faculty = await _unitOfWork.Fauclties.GetByIdAsync(request.Id);

        if (faculty is null)
            return Result.Failure(_errors.NotFound);

        faculty.IsDeleted = !faculty.IsDeleted;

        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();
    }
}
