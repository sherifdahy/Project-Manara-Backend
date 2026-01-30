using App.Application.Commands.Faculties;
using App.Application.Contracts.Responses.Faculties;

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
        if (_unitOfWork.Fauclties.IsExist(x => x.Name == request.Name))
            return Result.Failure<FacultyResponse>(_facultyerrors.DuplicatedName);

        if(!_unitOfWork.Universities.IsExist(x=>x.Id==request.UniversityId))
            return Result.Failure<FacultyResponse>(_universityErrors.NotFound);

        var faculty = request.Adapt<Faculty>();

        faculty.UniversityId = request.UniversityId;

        await _unitOfWork.Fauclties.AddAsync(faculty,cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success(faculty.Adapt<FacultyResponse>());
    }
}
