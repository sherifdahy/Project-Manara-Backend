using App.Application.Commands.Roles;
using App.Application.Contracts.Responses.Roles;
using App.Infrastructure.Abstractions.Consts;
using System.Data;

namespace App.Application.Handlers.Commands.Roles;

public class CreateRoleCommandHandler(RoleManager<ApplicationRole> roleManager
    , IUnitOfWork unitOfWork,
    RoleErrors roleErrors
    , PermissionErrors permissionErrors) : IRequestHandler<CreateRoleCommand, Result<RoleDetailResponse>>
{
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly RoleErrors _roleErrors = roleErrors;
    private readonly PermissionErrors _permissionErrors = permissionErrors;

    public async Task<Result<RoleDetailResponse>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {

        var allowedPermissions = Permissions.GetAllPermissions();

        if (request.Permissions.Except(allowedPermissions).Any())
            return Result.Failure<RoleDetailResponse>(_permissionErrors.InvalidPermissions);

        if (request.RoleId.HasValue && await _roleManager.FindByIdAsync(request.RoleId.ToString()!) is null)
            return Result.Failure<RoleDetailResponse>(_roleErrors.NotFound);

        var newRole = new ApplicationRole()
        {
            Name = request.Name,
            Code = request.Code,
            RoleId = request.RoleId,
            Description = request.Description,
            IsDeleted = request.IsDeleted,
            ConcurrencyStamp = Guid.NewGuid().ToString(),
        };

        var result = await _roleManager.CreateAsync(newRole);

        if (result.Succeeded)
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
            (newRole.Id,
                newRole.Name,
                newRole.Description,
                newRole.Code,
                newRole.IsDeleted,
                0,
                request.Permissions,
                request.RoleId != null ? (await _roleManager.FindByIdAsync(request.RoleId.ToString()!)).Adapt<RoleResponse>() : null
            ));
        }

        var error = result.Errors.First();

        return Result.Failure<RoleDetailResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
    }
}