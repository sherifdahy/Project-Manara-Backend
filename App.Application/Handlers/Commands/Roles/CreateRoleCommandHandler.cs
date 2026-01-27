using App.Application.Abstractions;
using App.Application.Commands.Roles;
using App.Application.Errors;
using App.Application.Responses.Role;
using App.Core.Entities.Identity;
using App.Core.Enums;
using App.Infrastructure.Abstractions.Consts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SA.Accountring.Core.Entities.Interfaces;
using System.Data;
using System.Security.Claims;

namespace App.Application.Handlers.Commands.Roles;

public class CreateRoleCommandHandler(RoleManager<ApplicationRole> roleManager
    ,IUnitOfWork unitOfWork
    , RoleErrors roleErrors
    ,PermissionErrors permissionErrors
    ,UniversityErrors universityErrors) : IRequestHandler<CreateRoleCommand, Result<RoleDetailResponse>>
{
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly RoleErrors _roleErrors = roleErrors;
    private readonly PermissionErrors _permissionErrors= permissionErrors;
    private readonly UniversityErrors _universityErrors = universityErrors;

    public async Task<Result<RoleDetailResponse>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var existRole = await _roleManager.FindByNameAsync(request.Name);

        if (existRole is not null)
            return Result.Failure<RoleDetailResponse>(_roleErrors.Duplicated);

        var allowedPermissions = Permissions.GetAllPermissions();

        if (request.Permissions.Except(allowedPermissions).Any())
            return Result.Failure<RoleDetailResponse>(_permissionErrors.InvalidPermissions);

        if (!(_unitOfWork.Universities.IsExist(x => x.Id == request.UniversityId)))
            return Result.Failure<RoleDetailResponse>(_universityErrors.InvalidId);

        if (!Enum.IsDefined(typeof(RoleType), request.RoleType))
            return Result.Failure<RoleDetailResponse>(_permissionErrors.InvalidType);


        var newRole = new ApplicationRole()
        {
            Name = request.Name,
            UniversityId= request.UniversityId,
            RoleType= request.RoleType,
            ConcurrencyStamp = Guid.NewGuid().ToString(),
        };

        var result = await _roleManager.CreateAsync(newRole);

        if(result.Succeeded)
        {
            var roleClaims = new List<IdentityRoleClaim<int>>();

            foreach (var permission in request.Permissions)
            {
                roleClaims.Add(new IdentityRoleClaim<int>
                {
                    RoleId = newRole.Id,
                    ClaimType = Permissions.Type,
                    ClaimValue = permission
                });
            }


            await _unitOfWork.RoleClaims.AddRangeAsync(roleClaims, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);

            return Result.Success(new RoleDetailResponse
            (   newRole.Id,
                newRole.Name,
                newRole.IsDeleted,
                newRole.UniversityId,
                newRole!.RoleType,
                request.Permissions
            ));
        }

        var error = result.Errors.First();

        return Result.Failure<RoleDetailResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
    }
}
