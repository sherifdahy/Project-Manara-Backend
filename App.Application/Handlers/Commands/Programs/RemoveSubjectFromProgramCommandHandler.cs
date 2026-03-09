using App.Application.Commands.Programs;
using App.Core.Entities.Relations;
using App.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Handlers.Commands.Programs;

public class RemoveSubjectFromProgramCommandHandler(IUnitOfWork unitOfWork, ProgramErrors programErrors, SubjectErrors subjectErrors) : IRequestHandler<RemoveSubjectFromProgramCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ProgramErrors _programErrors = programErrors;
    private readonly SubjectErrors _subjectErrors = subjectErrors;
    public async Task<Result> Handle(RemoveSubjectFromProgramCommand request, CancellationToken cancellationToken)
    {
        if (!await _unitOfWork.Programs.IsExistAsync(x => x.Id == request.ProgramId, cancellationToken))
            return Result.Failure(_programErrors.NotFound);

        if (!await _unitOfWork.Subjects.IsExistAsync(x => x.Id == request.SubjectId, cancellationToken))
            return Result.Failure(_subjectErrors.NotFound);

        var programSubject = new ProgramSubject
        {
            ProgramId = request.ProgramId,
            SubjectId = request.SubjectId
        };

        _unitOfWork.ProgramSubjects.Delete(programSubject);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();
    }
}
