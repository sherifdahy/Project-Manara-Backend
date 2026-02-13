using App.Application.Contracts.Responses.Departments;
using App.Application.Contracts.Responses.Programs;
using App.Application.Errors;
using App.Application.Queries.Departments;
using App.Application.Queries.Programs;
using App.Core.Entities.Personnel;
using App.Core.Extensions;
using App.Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Handlers.Queries.Programs;

public class GetMyProgramQueryHandler(IUnitOfWork unitOfWork
    ,IHttpContextAccessor httpContextAccessor
    ,ProgramErrors programErrors) : IRequestHandler<GetMyProgramQuery, Result<ProgramDetailResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor = httpContextAccessor;
    private readonly ProgramErrors _programErrors = programErrors;


    public async Task<Result<ProgramDetailResponse>> Handle(GetMyProgramQuery request, CancellationToken cancellationToken)
    {
        var userId = _contextAccessor.HttpContext!.User.GetUserId();

        var programUser = await _unitOfWork.ProgramUsers.FindAsync(x => x.UserId == userId, i => i.Include(d=>d.Program), cancellationToken);

        if (programUser == null)
            return Result.Failure<ProgramDetailResponse>(_programErrors.NotFoundForCurrentUser);

        var response = programUser.Program.Adapt<ProgramDetailResponse>();

        return Result.Success(response);
    }
}