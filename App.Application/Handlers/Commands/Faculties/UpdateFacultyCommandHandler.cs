using App.Application.Commands.Faculties;
using App.Application.Contracts.Responses.Faculties;
using App.Application.Errors;

namespace App.Application.Handlers.Commands.Faculties;

public class UpdateFacultyCommandHandler(IUnitOfWork unitOfWork
    ,FacultyErrors facultyErrors
    ,UniversityErrors universityErrors) : IRequestHandler<UpdateFacultyCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly FacultyErrors _facultyErrors = facultyErrors;
    private readonly UniversityErrors _universityErrors = universityErrors;

    public async Task<Result> Handle(UpdateFacultyCommand request, CancellationToken cancellationToken)
    {
        //TODO Make it inside the university
        if (_unitOfWork.Fauclties.IsExist(x => x.Name == request.Name && x.Id != request.Id))
            return Result.Failure(_facultyErrors.DuplicatedName);

        var faculty = await _unitOfWork.Fauclties.GetByIdAsync(request.Id, cancellationToken);

        if (faculty == null)
            return Result.Failure(_facultyErrors.NotFound);

        request.Adapt(faculty);

        _unitOfWork.Fauclties.Update(faculty);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();
    }
}
