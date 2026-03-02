using App.Application.Commands.ProgramUsers;
using App.Application.Contracts.Responses.ProgramUsers;
using App.Core.Consts;
using App.Core.Entities.Personnel;

namespace App.Application.Handlers.Commands.ProgramUsers;

public class CreateProgramUserCommandHandler(IUnitOfWork unitOfWork
    ,ProgramErrors programErrors
    ,UserManager<ApplicationUser> userManager
    ,UserErrors userErrors
    ,RoleManager<ApplicationRole> roleManager
    ,RoleErrors roleErrors) : IRequestHandler<CreateProgramUserCommand, Result<ProgramUserResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ProgramErrors _programErrors = programErrors;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly UserErrors _userErrors = userErrors;
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
    private readonly RoleErrors _roleErrors = roleErrors;

    public async Task<Result<ProgramUserResponse>> Handle(CreateProgramUserCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.Programs.GetByIdAsync(request.ProgramId) is null)
            return Result.Failure<ProgramUserResponse>(_programErrors.NotFound);

        if (await _userManager.FindByEmailAsync(request.Email) is not null)
            return Result.Failure<ProgramUserResponse>(_userErrors.DuplicatedEmail);


        foreach (var role in request.Roles)
        {
            var roleEntity = await _roleManager.FindByNameAsync(role);

            if (roleEntity is null)
                return Result.Failure<ProgramUserResponse>(_roleErrors.NotFound);

            if (roleEntity.ScopeId != DefaultScopes.Program.Id)
                return Result.Failure<ProgramUserResponse>(_roleErrors.ScopeIsNotValidForRole);

        }

        var applicationUser = request.Adapt<ApplicationUser>();

        applicationUser.UserName = request.Email;

        var creationResult = await _userManager.CreateAsync(applicationUser, request.Password);

        if (creationResult.Succeeded)
        {
            var roleAssignResult = await _userManager.AddToRolesAsync(applicationUser, request.Roles);

            if (roleAssignResult.Succeeded)
            {
                var programUser = new ProgramUser()
                {
                    ProgramId = request.ProgramId,
                    UserId = applicationUser.Id,
                };

                await _unitOfWork.ProgramUsers.AddAsync(programUser, cancellationToken);
                await _unitOfWork.SaveAsync(cancellationToken);

                var response = applicationUser.Adapt<ProgramUserResponse>();

                response.Gender = programUser.User.Gender;
                response.NationalId = programUser.User.NationalId;
                response.BirthDate = programUser.User.BirthDate;

                response.Roles = request.Roles;

                return Result.Success(response);
            }

            var rolesAssignError = creationResult.Errors.First();

            return Result.Failure<ProgramUserResponse>(new Error(rolesAssignError.Code, rolesAssignError.Description, StatusCodes.Status400BadRequest));
        }

        var error = creationResult.Errors.First();

        return Result.Failure<ProgramUserResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
    }
}

