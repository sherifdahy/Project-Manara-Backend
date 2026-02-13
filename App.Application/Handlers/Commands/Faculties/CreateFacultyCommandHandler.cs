using App.Application.Commands.Faculties;
using App.Application.Contracts.Responses.Departments;
using App.Application.Contracts.Responses.Faculties;
using App.Application.Errors;

namespace App.Application.Handlers.Commands.Faculties;

public class CreateFacultyCommandHandler(IUnitOfWork unitOfWork
    ,FacultyErrors facultyerrors
    ,UniversityErrors universityErrors) : IRequestHandler<CreateFacultyCommand, Result<FacultyResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly FacultyErrors _facultyerrors = facultyerrors;
    private readonly UniversityErrors _universityErrors = universityErrors;

    public async Task<Result<FacultyResponse>> Handle(CreateFacultyCommand request, CancellationToken cancellationToken)
    {
        var isUniversityExists = await _unitOfWork.Universities.IsExistAsync(f => f.Id == request.UniversityId);

        if (!isUniversityExists)
            return Result.Failure<FacultyResponse>(_universityErrors.NotFound);

        var isFacultyExists = await _unitOfWork.Fauclties.IsExistAsync(x => x.UniversityId == request.UniversityId && x.Name == request.Name);

        if (isFacultyExists)
            return Result.Failure<FacultyResponse>(_facultyerrors.DuplicatedName);


        var faculty = request.Adapt<Faculty>();

        faculty.UniversityId = request.UniversityId;

        await _unitOfWork.Fauclties.AddAsync(faculty,cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success(faculty.Adapt<FacultyResponse>());
    }
}
