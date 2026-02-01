using App.Application.Commands.Roles;
using App.Application.Contracts.Responses.Roles;
using App.Application.Errors;
using App.Infrastructure.Abstractions.Consts;
using App.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Handlers.Commands.Roles;

public class ToggleStatusPermissionRoleCommandHandler(IUnitOfWork unitOfWork
    ,PermissionErrors permissionErrors
    ,FacultyErrors facultyErrors) : IRequestHandler<ToggleStatusPermissionRoleCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly PermissionErrors _permissionErrors = permissionErrors;
    private readonly FacultyErrors _facultyErrors = facultyErrors;

    public async Task<Result> Handle(ToggleStatusPermissionRoleCommand request, CancellationToken cancellationToken)
    {

        var allowedPermissions = Permissions.GetAllPermissions();

        if (!allowedPermissions.Contains(request.ClaimValue))
            return Result.Failure<AssignToRolePermissionResponse>(_permissionErrors.InvalidPermissions);

        var roleClaimOverride = await _unitOfWork.RoleClaimOverrides
            .FindAsync(rc => rc.RoleId == request.RoleId && rc.ClaimValue == request.ClaimValue && rc.FacultyId == request.FacultyId);

        if (roleClaimOverride == null)
            return Result.Failure(_permissionErrors.OverridePermissionNotFound);


        if (request.User.GetFacultyId() != null && request.User.GetFacultyId() != request.FacultyId)
            return Result.Failure<AssignToRolePermissionResponse>(_facultyErrors.NotAllowedFaculty);

        if (request.User.GetUniversityId() != null && !_unitOfWork.Fauclties
                .IsExist(f => f.Id == request.FacultyId && f.UniversityId == request.User.GetUniversityId()))
            return Result.Failure<AssignToRolePermissionResponse>(_facultyErrors.NotAllowedFaculty);


        roleClaimOverride.IsAllowed = !roleClaimOverride.IsAllowed;

        _unitOfWork.RoleClaimOverrides.Update(roleClaimOverride);
        await _unitOfWork.SaveAsync();

        return Result.Success();
    }
}
