using App.Application.Commands.ProgramUsers;
using App.Core.Consts;

namespace App.Application.Handlers.Commands.ProgramUsers;

public class UpdateProgramUserCommandHandler(IUnitOfWork unitOfWork
    ,UserErrors userErrors
    ,IProgramService programService
    ,IHttpContextAccessor httpContextAccessor
    ,UserManager<ApplicationUser> userManager
    ,RoleManager<ApplicationRole> roleManager
    ,RoleErrors roleErrors) : IRequestHandler<UpdateProgramUserCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserErrors _userErrors = userErrors;
    private readonly IProgramService _programService = programService;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
    private readonly RoleErrors _roleErrors = roleErrors;




    public async Task<Result> Handle(UpdateProgramUserCommand request, CancellationToken cancellationToken)
    {
        var programUser = await _unitOfWork.ProgramUsers
            .FindAsync(x => x.UserId == request.UserId, i => i.Include(p => p.User), cancellationToken);

        if (programUser == null)
            return Result.Failure(_userErrors.NotFound);

        if (!await _programService.IsUserHasAccessToProgram(_httpContextAccessor.HttpContext!.User, programUser.ProgramId))
            return Result.Failure(_userErrors.Forbidden);


        if (_userManager.Users.Any(x => x.Email == request.Email && x.Id != request.UserId))
            return Result.Failure(_userErrors.DuplicatedEmail);

        foreach (var role in request.Roles)
        {

            var roleEntity = await _roleManager.FindByNameAsync(role);

            if (roleEntity is null)
                return Result.Failure(_roleErrors.NotFound);

            if (roleEntity.ScopeId != DefaultScopes.Program.Id)
                return Result.Failure(_roleErrors.ScopeIsNotValidForRole);
        }

        request.Adapt(programUser.User);

        var updateUserResult = await _userManager.UpdateAsync(programUser.User);

        if (updateUserResult.Succeeded)
        {
            var currentRolesAsString = await _userManager.GetRolesAsync(programUser.User);

            var newRolesAsString = request.Roles.Except(currentRolesAsString).ToList();

            var removedRolesAsString = currentRolesAsString.Except(request.Roles).ToList();

            if (newRolesAsString.Any())
            {
                var addResult = await _userManager.AddToRolesAsync(programUser.User, newRolesAsString);
                if (!addResult.Succeeded)
                {
                    var err = addResult.Errors.First();
                    return Result.Failure(new Error(err.Code, err.Description, StatusCodes.Status400BadRequest));
                }
            }

            if (removedRolesAsString.Any())
            {
                var removeResult = await _userManager.RemoveFromRolesAsync(programUser.User, removedRolesAsString);
                if (!removeResult.Succeeded)
                {
                    var err = removeResult.Errors.First();
                    return Result.Failure(new Error(err.Code, err.Description, StatusCodes.Status400BadRequest));
                }
            }

            return Result.Success();
        }

        var updateUserError = updateUserResult.Errors.First();

        return Result.Failure(new Error(updateUserError.Code, updateUserError.Description, StatusCodes.Status400BadRequest));
    }
}