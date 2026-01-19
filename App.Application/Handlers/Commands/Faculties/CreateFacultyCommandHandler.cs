using App.Application.Commands.Faculties;
using App.Application.Responses.Faculties;

namespace App.Application.Handlers.Commands.Faculties;

public class CreateFacultyCommandHandler(IUnitOfWork unitOfWork,FacultyErrors errors) : IRequestHandler<CreateFacultyCommand, Result<FacultyResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly FacultyErrors _errors = errors;

    public async Task<Result<FacultyResponse>> Handle(CreateFacultyCommand request, CancellationToken cancellationToken)
    {
        if (_unitOfWork.Fauclties.IsExist(x => x.Name == request.Name))
            return Result.Failure<FacultyResponse>(_errors.DuplicatedName);

        var faculty = request.Adapt<Faculty>();

        faculty.UniversityId = request.UniversityId;

        await _unitOfWork.Fauclties.AddAsync(faculty,cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success(faculty.Adapt<FacultyResponse>());
    }
}
