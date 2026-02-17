using App.Application.Commands.DepartmentUsers;
using App.Application.Contracts.Responses.DepartmentUsers;
using App.Core.Consts;
using App.Core.Entities.Personnel;

namespace App.Application.Handlers.Commands.DepartmentUsers;

public class CreateDepartmentUserCommandHandler(IUnitOfWork unitOfWork
    ,DepartmentErrors departmentErrors
    ,UserManager<ApplicationUser> userManager
    ,UserErrors userErrors
    ,RoleManager<ApplicationRole> roleManager
    ,RoleErrors roleErrors) : IRequestHandler<CreateDepartmentUserCommand, Result<DepartmentUserResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly DepartmentErrors _departmentErrors = departmentErrors;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly UserErrors _userErrors = userErrors;
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
    private readonly RoleErrors _roleErrors = roleErrors;

    public async Task<Result<DepartmentUserResponse>> Handle(CreateDepartmentUserCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.Departments.GetByIdAsync(request.DepartmentId) is null)
            return Result.Failure<DepartmentUserResponse>(_departmentErrors.NotFound);

        if (await _userManager.FindByEmailAsync(request.Email) is not null)
            return Result.Failure<DepartmentUserResponse>(_userErrors.DuplicatedEmail);

        foreach (var role in request.Roles)
        {
            var roleEntity = await _roleManager.FindByNameAsync(role);

            if (roleEntity is null)
                return Result.Failure<DepartmentUserResponse>(_roleErrors.NotFound);

            if (roleEntity.ScopeId != DefaultScopes.Department.Id)
                return Result.Failure<DepartmentUserResponse>(_roleErrors.ScopeIsNotValidForRole);

        }

        var applicationUser = request.Adapt<ApplicationUser>();

        applicationUser.UserName = request.Email;

        var creationResult = await _userManager.CreateAsync(applicationUser, request.Password);

        if (creationResult.Succeeded)
        {
            var roleAssignResult = await _userManager.AddToRolesAsync(applicationUser, request.Roles);

            if (roleAssignResult.Succeeded)
            {
                var departmentUser = new DepartmentUser()
                {
                    DepartmentId = request.DepartmentId,
                    UserId = applicationUser.Id,
                };

                await _unitOfWork.DepartmentUsers.AddAsync(departmentUser, cancellationToken);
                await _unitOfWork.SaveAsync(cancellationToken);

                var response = applicationUser.Adapt<DepartmentUserResponse>();

                response.Roles = request.Roles;

                return Result.Success(response);
            }

            var rolesAssignError = creationResult.Errors.First();

            return Result.Failure<DepartmentUserResponse>(new Error(rolesAssignError.Code, rolesAssignError.Description, StatusCodes.Status400BadRequest));
        }

        var error = creationResult.Errors.First();

        return Result.Failure<DepartmentUserResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
    }
}

