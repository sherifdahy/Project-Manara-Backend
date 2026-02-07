using App.Application.Commands.Roles;
using App.Application.Contracts.Responses.Roles;
using App.Core.Extensions;
using App.Infrastructure.Abstractions.Consts;
using System.Data;

namespace App.Application.Handlers.Commands.Roles;

public class AssignPermissionsToRoleCommandHandler(IUnitOfWork unitOfWork
    ,PermissionErrors permissionErrors
    ,FacultyErrors facultyErrors
    ,RoleErrors roleErrors
    ,RoleManager<ApplicationRole> roleManager
    ,IAuthenticationService authenticationService) : IRequestHandler<AssignPermissionsToRoleCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly PermissionErrors _permissionErrors = permissionErrors;
    private readonly FacultyErrors _facultyErrors = facultyErrors;
    private readonly RoleErrors _roleErrors = roleErrors;
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
    private readonly IAuthenticationService _authenticationService = authenticationService;

    public async Task<Result> Handle(AssignPermissionsToRoleCommand request, CancellationToken cancellationToken)
    {

        var isFacultyExists = _unitOfWork.Fauclties
            .IsExist(f => f.Id == request.FacultyId);

        if (!isFacultyExists)
            return Result.Failure(_facultyErrors.NotFound);


        if (await _roleManager.FindByIdAsync(request.RoleId.ToString()) is not { } role)
            return Result.Failure(_roleErrors.NotFound);



        var emptyClaimValues = request.ClaimValues?
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToList();

        if (emptyClaimValues == null || emptyClaimValues.Count == 0)
        {
            var entities = await _unitOfWork.RoleClaimOverrides
                .FindAllAsync(rco => rco.RoleId == request.RoleId && rco.FacultyId == request.FacultyId);

            _unitOfWork.RoleClaimOverrides.DeleteRange(entities);
            await _unitOfWork.SaveAsync();

            return Result.Success();
        }

        var defaultRolePermissions = await _roleManager.GetClaimsAsync(role);

        foreach (var claimValue in request.ClaimValues!)
        {
            if (!defaultRolePermissions.Select(x => x.Value).Contains(claimValue))
                return Result.Failure(_permissionErrors.InvalidPermissions);
        }



        //4.0 TODO Must The Role To be less than the userRole (Must Be Attribute)


        var currentRolePermissionsOverride = await _unitOfWork.RoleClaimOverrides
            .FindAllAsync(rco => rco.RoleId == request.RoleId && rco.FacultyId == request.FacultyId);

        var newRolePermissionsOverride = request.ClaimValues.Except(currentRolePermissionsOverride.Select(x => x.ClaimValue));


        foreach (var newRolePermissionOverride in newRolePermissionsOverride)
        {
           await  _unitOfWork.RoleClaimOverrides
                .AddAsync(new RoleClaimOverride { ClaimValue=newRolePermissionOverride,RoleId=request.RoleId,FacultyId=request.FacultyId});
        }

        var removedRolePermissionsOverride = currentRolePermissionsOverride
            .Select(x => x.ClaimValue).Except(request.ClaimValues);


        foreach(var removedRolePermissionOverride in removedRolePermissionsOverride)
        {
            var entityToRemove = currentRolePermissionsOverride.First(x => x.ClaimValue == removedRolePermissionOverride);
            _unitOfWork.RoleClaimOverrides.Delete(entityToRemove);
        }

        await _unitOfWork.SaveAsync();

        return Result.Success();

    }
}




#region MyRegion
//var isRoleOverrideExists = _unitOfWork.RoleClaimOverrides
//    .IsExist(rc => rc.RoleId == request.RoleId && rc.ClaimValue == request.ClaimValue && rc.FacultyId == request.FacultyId);

//if (isRoleOverrideExists)
//    return Result.Failure<AssignToRolePermissionResponse>(_permissionErrors.DuplicatedPermissionForRole);

//var isFacultyExists = _unitOfWork.Fauclties
//    .IsExist(f => f.Id == request.FacultyId);

//if (!isFacultyExists)
//    return Result.Failure<AssignToRolePermissionResponse>(_facultyErrors.NotFound);


//var allowedPermissions = Permissions.GetAllPermissions();

//foreach(var claimValue in request.ClaimValues)
//{
//    if (!allowedPermissions.Contains(claimValue))
//        return Result.Failure<AssignToRolePermissionResponse>(_permissionErrors.InvalidPermissions);
//}



//if (await _roleManager.FindByIdAsync(request.RoleId.ToString()) is not { } role)
//    return Result.Failure<AssignToRolePermissionResponse>(_roleErrors.NotFound);

//var rolePermissions = await _roleManager.GetClaimsAsync(role);

//if (rolePermissions.Select(rp => rp.Value).Contains(request.ClaimValue) && request.IsAllowed==true)
//    return Result.Failure<AssignToRolePermissionResponse>(_permissionErrors.DuplicatedPermissionForRole);


//var roleClaimOverride = new RoleClaimOverride()
//{
//    RoleId = request.RoleId,
//    ClaimValue = request.ClaimValue,
//    FacultyId = request.FacultyId,
//    IsAllowed=request.IsAllowed,
//};

//await _unitOfWork.RoleClaimOverrides.AddAsync(roleClaimOverride);
//await _unitOfWork.SaveAsync();

//return Result.Success<AssignToRolePermissionResponse>(roleClaimOverride.Adapt<AssignToRolePermissionResponse>()); 
#endregion