using App.Application.Commands.Roles;
using App.Application.Contracts.Responses.Roles;
using App.Application.Errors;
using App.Infrastructure.Abstractions.Consts;
using App.Infrastructure.Extensions;
using App.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace App.Application.Handlers.Commands.Roles;

public class AssignPermissionToRoleCommandHandler(IUnitOfWork unitOfWork
    ,PermissionErrors permissionErrors
    ,FacultyErrors facultyErrors
    ,RoleErrors roleErrors
    ,RoleManager<ApplicationRole> roleManager
    ,IAuthenticationService authenticationService) : IRequestHandler<AssignPermissionToRoleCommand, Result<AssignToRolePermissionResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly PermissionErrors _permissionErrors = permissionErrors;
    private readonly FacultyErrors _facultyErrors = facultyErrors;
    private readonly RoleErrors _roleErrors = roleErrors;
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
    private readonly IAuthenticationService _authenticationService = authenticationService;

    public async Task<Result<AssignToRolePermissionResponse>> Handle(AssignPermissionToRoleCommand request, CancellationToken cancellationToken)
    {
        var isRoleOverrideExists = _unitOfWork.RoleClaimOverrides
            .IsExist(rc => rc.RoleId == request.RoleId && rc.ClaimValue == request.ClaimValue && rc.FacultyId == request.FacultyId);

        if (isRoleOverrideExists)
            return Result.Failure<AssignToRolePermissionResponse>(_permissionErrors.DuplicatedPermissionForRole);

        var isFacultyExists = _unitOfWork.Fauclties
            .IsExist(f => f.Id == request.FacultyId);

        if (!isFacultyExists)
            return Result.Failure<AssignToRolePermissionResponse>(_facultyErrors.NotFound);


        if (request.User.GetFacultyId() != null && request.User.GetFacultyId() != request.FacultyId)
            return Result.Failure<AssignToRolePermissionResponse>(_facultyErrors.NotAllowedFaculty);


        if (request.User.GetUniversityId() != null && !_unitOfWork.Fauclties
                .IsExist(f => f.Id == request.FacultyId && f.UniversityId == request.User.GetUniversityId()))
            return Result.Failure<AssignToRolePermissionResponse>(_facultyErrors.NotAllowedFaculty);

        var allowedPermissions = Permissions.GetAllPermissions();

        if (!allowedPermissions.Contains(request.ClaimValue))
            return Result.Failure<AssignToRolePermissionResponse>(_permissionErrors.InvalidPermissions);


        if (await _roleManager.FindByIdAsync(request.RoleId.ToString()) is not { } role)
            return Result.Failure<AssignToRolePermissionResponse>(_roleErrors.NotFound);

        var rolePermissions = await _roleManager.GetClaimsAsync(role);

        if (rolePermissions.Select(rp => rp.Value).Contains(request.ClaimValue) && request.IsAllowed==true)
            return Result.Failure<AssignToRolePermissionResponse>(_permissionErrors.DuplicatedPermissionForRole);


        var roleClaimOverride = new RoleClaimOverride()
        {
            RoleId = request.RoleId,
            ClaimValue = request.ClaimValue,
            FacultyId = request.FacultyId,
            IsAllowed=request.IsAllowed,
        };

        await _unitOfWork.RoleClaimOverrides.AddAsync(roleClaimOverride);
        await _unitOfWork.SaveAsync();

        return Result.Success<AssignToRolePermissionResponse>(roleClaimOverride.Adapt<AssignToRolePermissionResponse>());

    }
}