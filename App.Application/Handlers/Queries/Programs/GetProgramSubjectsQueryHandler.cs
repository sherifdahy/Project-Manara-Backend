using App.Application.Contracts.Responses.Subjects;
using App.Application.Queries.Programs;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Handlers.Queries.Programs;

public class GetProgramSubjectsQueryHandler(IUnitOfWork unitOfWork,ProgramErrors programErrors) : IRequestHandler<GetProgramSubjectsQuery, Result<List<SubjectResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ProgramErrors _programErrors = programErrors;

    public async Task<Result<List<SubjectResponse>>> Handle(GetProgramSubjectsQuery request, CancellationToken cancellationToken)
    {
        if (!await _unitOfWork.Programs.IsExistAsync(x => x.Id == request.ProgramId, cancellationToken))
            return Result.Failure<List<SubjectResponse>>(_programErrors.NotFound);

        var programSubjects = await _unitOfWork.ProgramSubjects.FindAllAsync(x => x.ProgramId == request.ProgramId,x=>x.Include(w=>w.Subject), cancellationToken);

        return Result.Success(programSubjects.Select(x=>x.Subject).Adapt<List<SubjectResponse>>());
    }
}
