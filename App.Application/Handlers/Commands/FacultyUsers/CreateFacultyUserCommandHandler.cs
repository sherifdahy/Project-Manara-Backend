using App.Application.Commands.FacultyUsers;
using App.Application.Contracts.Responses.FacultyUsers;
using App.Application.Contracts.Responses.Roles;
using App.Core.Entities.Personnel;

namespace App.Application.Handlers.Commands.FacultyUsers;

public class CreateFacultyUserCommandHandler(
    FacultyErrors facultyErrors,
    RoleErrors roleErrors,
    IUnitOfWork unitOfWork,
    RoleManager<ApplicationRole> roleManager,
    UserManager<ApplicationUser> userManager) : IRequestHandler<CreateFacultyUserCommand, Result<FacultyUserResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly FacultyErrors _facultyErrors = facultyErrors;
    private readonly RoleErrors _roleErrors = roleErrors;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
    public async Task<Result<FacultyUserResponse>> Handle(CreateFacultyUserCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.Fauclties.GetByIdAsync(request.FacultyId) is null)
            return Result.Failure<FacultyUserResponse>(_facultyErrors.NotFound);

        if (await _userManager.FindByEmailAsync(request.Email) is not null)
            return Result.Failure<FacultyUserResponse>(UserErrors.DuplicatedEmail);

        foreach(var role in request.Roles)
        {
            var result = await _roleManager.FindByNameAsync(role);
            
            if(result is null)
                return Result.Failure<FacultyUserResponse>(_roleErrors.NotFound);
        }

        var applicationUser = request.Adapt<ApplicationUser>();

        applicationUser.UserName = request.Email;

        var creationResult = await _userManager.CreateAsync(applicationUser,request.Password);

        if(creationResult.Succeeded)
        {
            var roleAssignResult = await _userManager.AddToRolesAsync(applicationUser,request.Roles);

            if(roleAssignResult.Succeeded)
            {
                var facultyUser = new FacultyUser()
                {
                    FacultyId = request.FacultyId,
                    UserId = applicationUser.Id,
                };

                await _unitOfWork.FacultyUsers.AddAsync(facultyUser, cancellationToken);
                await _unitOfWork.SaveAsync(cancellationToken);

                var response = applicationUser.Adapt<FacultyUserResponse>();

                response.Roles = request.Roles;

                return Result.Success(response);
            }

            var rolesAssignError = creationResult.Errors.First();

            return Result.Failure<FacultyUserResponse>(new Error(rolesAssignError.Code, rolesAssignError.Description, StatusCodes.Status400BadRequest));
        }

        var error = creationResult.Errors.First();

        return Result.Failure<FacultyUserResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
    }
}
