

using App.Application.Commands.Subjects;
using App.Application.Errors;

namespace App.Application.Handlers.Commands.Subjects;

public class ToggleStatusSubjectCommandHandler(IUnitOfWork unitOfWork,SubjectErrors subjectErrors) : IRequestHandler<ToggleStatusSubjectCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly SubjectErrors _subjectErrors = subjectErrors;


    public async Task<Result> Handle(ToggleStatusSubjectCommand request, CancellationToken cancellationToken)
    {
        var subject = await _unitOfWork.Subjects
            .FindAsync(
            criteria: x => x.Id == request.Id,
            includes: q => q.Include(s => s.Prerequisites)
        );

        if (subject is null)
            return Result.Failure(_subjectErrors.NotFound);


        subject.IsDeleted=!subject.IsDeleted;

        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();
    }
}
