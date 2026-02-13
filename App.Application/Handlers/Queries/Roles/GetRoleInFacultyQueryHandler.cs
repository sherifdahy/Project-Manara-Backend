using App.Application.Contracts.Responses.Roles;
using App.Application.Queries.Roles;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Handlers.Queries.Roles;

public class GetRoleInFacultyQueryHandler(RoleManager<ApplicationRole> roleManager
    ,RoleErrors roleErrors
    ,IUnitOfWork unitOfWork) : IRequestHandler<GetRoleInFacultyQuery, Result<GetRoleInFacultyResponse>>
{
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
    private readonly RoleErrors _roleErrors = roleErrors;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<GetRoleInFacultyResponse>> Handle(GetRoleInFacultyQuery request, CancellationToken cancellationToken)
    {
        if (await _roleManager.FindByIdAsync(request.RoleId.ToString()) is not { } role)
            return Result.Failure<GetRoleInFacultyResponse>(_roleErrors.NotFound);

        var defaultPermissions = await _roleManager.GetClaimsAsync(role);

        var overridePermissions = await _unitOfWork.RoleClaimOverrides
            .FindAllAsync(rco=>rco.RoleId == request.RoleId && rco.FacultyId==request.FacultyId,cancellationToken);

        var response = new GetRoleInFacultyResponse
        (
            role.Id,
            role.Name!,
            role.Code,
            role.Description,
            role.IsDeleted,
            defaultPermissions.Select(x=>x.Value),
            overridePermissions.Select(x => x.ClaimValue)
        );

        return Result.Success<GetRoleInFacultyResponse>(response);

    }
}
