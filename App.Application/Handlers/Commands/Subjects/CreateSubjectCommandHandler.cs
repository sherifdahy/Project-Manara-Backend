using App.Application.Commands.Subjects;
using App.Application.Contracts.Responses.Subjects;

namespace App.Application.Handlers.Commands.Subjects;

public class CreateSubjectCommandHandler(IUnitOfWork unitOfWork
    ,FacultyErrors facultyErrors
    ,SubjectErrors subjectErrors)
    : IRequestHandler<CreateSubjectCommand, Result<SubjectResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly FacultyErrors _facultyErrors = facultyErrors;
    private readonly SubjectErrors _subjectErrors = subjectErrors;

    public async Task<Result<SubjectResponse>> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
    {
        var isFacultyExists = await _unitOfWork.Fauclties.IsExistAsync(f => f.Id == request.FacultyId);

        if (!isFacultyExists)
            return Result.Failure<SubjectResponse>(_facultyErrors.NotFound);


        var isSubjectExists = await _unitOfWork.Subjects
            .IsExistAsync(x => x.FacultyId == request.FacultyId && x.Name == request.Name);

        if (isSubjectExists)
            return Result.Failure<SubjectResponse>(_subjectErrors.DuplicatedName);


        var subject = request.Adapt<Subject>();


        await _unitOfWork.Subjects.AddAsync(subject);


        foreach (var PrerequisiteId in request.PrerequisiteIds)
        {
            var isPrerequisiteSubject =  await _unitOfWork.Subjects
                .IsExistAsync(x => x.FacultyId == request.FacultyId && x.Id == PrerequisiteId);

            if (!isPrerequisiteSubject)
                return Result.Failure<SubjectResponse>(_subjectErrors.NotFound);

            subject.Prerequisites.Add(new SubjectPrerequisite
            {
                PrerequisiteId = PrerequisiteId
            });

        }
           

          await _unitOfWork.SaveAsync();

        return Result.Success<SubjectResponse>(subject.Adapt<SubjectResponse>());

    }
}

