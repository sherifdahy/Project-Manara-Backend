using App.Application.Contracts.Responses.Departments;
using App.Application.Contracts.Responses.Programs;
using App.Application.Errors;
using App.Application.Queries.Departments;
using App.Application.Queries.Programs;
using App.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Handlers.Queries.Programs;

public class GetProgramQueryHandler(IUnitOfWork unitOfWork,ProgramErrors programErrors) : IRequestHandler<GetProgramQuery, Result<ProgramDetailResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ProgramErrors _programErrors = programErrors;


    public async Task<Result<ProgramDetailResponse>> Handle(GetProgramQuery request, CancellationToken cancellationToken)
    {
        var program = await _unitOfWork.Programs.FindAsync(x => x.Id == request.Id, null, cancellationToken);

        if (program == null)
            return Result.Failure<ProgramDetailResponse>(_programErrors.NotFound);

        var response = program.Adapt<ProgramDetailResponse>();

        return Result.Success(response);

    }
}
