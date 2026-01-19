using App.Application.Commands.Faculties;

namespace App.Application.Handlers.Commands.Faculties;

public class UpdateFacultyCommandHandler(IUnitOfWork unitOfWork,FacultyErrors errors) : IRequestHandler<UpdateFacultyCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly FacultyErrors _errors = errors;

    public async Task<Result> Handle(UpdateFacultyCommand request, CancellationToken cancellationToken)
    {
        if (_unitOfWork.Fauclties.IsExist(x => x.Name == request.Name && x.Id != request.Id))
            return Result.Failure(_errors.DuplicatedName);

        var faculty = await _unitOfWork.Fauclties.GetByIdAsync(request.Id, cancellationToken);

        if (faculty == null)
            return Result.Failure(_errors.NotFound);

        request.Adapt(faculty);

        _unitOfWork.Fauclties.Update(faculty);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();
    }
}
