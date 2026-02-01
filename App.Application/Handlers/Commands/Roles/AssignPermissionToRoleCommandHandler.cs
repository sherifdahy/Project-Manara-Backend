using App.Application.Commands.Roles;
using App.Application.Contracts.Responses.Roles;
using App.Application.Errors;
using App.Infrastructure.Abstractions.Consts;
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
    ,RoleManager<ApplicationRole> roleManager) : IRequestHandler<AssignPermissionToRoleCommand, Result<AssignToRolePermissionResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly PermissionErrors _permissionErrors = permissionErrors;
    private readonly FacultyErrors _facultyErrors = facultyErrors;
    private readonly RoleErrors _roleErrors = roleErrors;
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;

    public Task<Result<AssignToRolePermissionResponse>> Handle(AssignPermissionToRoleCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    //public async Task<Result<AssignToRolePermissionResponse>> Handle(AssignPermissionToRoleCommand request, CancellationToken cancellationToken)
    //{
    //    //1.0 Check The Constrain Of The C.K
    //    var isRoleOverrideExists =  _unitOfWork.RoleClaimOverrides
    //        .IsExist(rc=>rc.RoleId== request.RoleId && rc.ClaimValue==request.ClaimValue && rc.FacultyId==request.FacultyId);

    //    if (isRoleOverrideExists)
    //        return Result.Failure<AssignToRolePermissionResponse>(_permissionErrors.DuplicatedPermissionForRole);
    //    //4.0 Check The exsists of the Faculty
    //    var isFacultyExists =  _unitOfWork.Fauclties
    //        .IsExist(f => f.Id == request.FacultyId);

    //    if (!isFacultyExists)
    //        return Result.Failure<AssignToRolePermissionResponse>(_facultyErrors.NotFound);



    //    //TODO
    //    //2.0 Check The FacultyId of the request same as The User



    //    //3.0 Check The Permision That Mathced The GetAllPrmissions
    //    var allowedPermissions = Permissions.GetAllPermissions();

    //    if (!allowedPermissions.Contains(request.ClaimValue))
    //        return Result.Failure<AssignToRolePermissionResponse>(_permissionErrors.InvalidPermissions);

    //    //4.0 Check The exsists of the Role

    //    if (await _roleManager.FindByIdAsync(request.RoleId.ToString()) is not { } role)
    //        return Result.Failure<AssignToRolePermissionResponse>(_roleErrors.NotFound);

    //    //5.0 If The Role Already Has The Permission And The IsAllowed == True
    //    var rolePermissions = await _roleManager.GetClaimsAsync(role);

    //    if (rolePermissions.Select(rp=>rp.Value).Contains(request.ClaimValue))
    //        return Result.Failure<AssignToRolePermissionResponse>(_permissionErrors.DuplicatedPermissionForRole);


    //    //TODO Complete
    //    var roleClaimOverride = new RoleClaimOverride()
    //    {
    //        RoleId= request.RoleId,
    //        ClaimValue= request.ClaimValue,
    //        FacultyId= request.FacultyId,
    //        ClaimValue = request?.ClaimValue
    //    };


    //}
}