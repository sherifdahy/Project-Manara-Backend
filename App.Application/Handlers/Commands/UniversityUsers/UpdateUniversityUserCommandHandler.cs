using App.Application.Commands.UniversityUsers;
using App.Application.Contracts.Responses.FacultyUsers;
using App.Core.Consts;


namespace App.Application.Handlers.Commands.UniversityUsers;

public class UpdateUniversityUserCommandHandler(IUniversityService universityService
    ,IUnitOfWork unitOfWork
    ,UserErrors userErrors
    , IHttpContextAccessor httpContextAccessor
    ,UserManager<ApplicationUser> userManager
    ,RoleManager<ApplicationRole> roleManager
    ,RoleErrors roleErrors) : IRequestHandler<UpdateUniversityUserCommand, Result>
{
    private readonly IUniversityService _universityService = universityService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserErrors _userErrors = userErrors;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
    private readonly RoleErrors _roleErrors = roleErrors;


    public async Task<Result> Handle(UpdateUniversityUserCommand request, CancellationToken cancellationToken)
    {
        var universityUser = await _unitOfWork.UniversityUsers
            .FindAsync(x => x.UserId == request.UserId, i => i.Include(p => p.User), cancellationToken);

        if (universityUser == null)
            return Result.Failure(_userErrors.NotFound);

        if (!await _universityService.IsUserHasAccessToUniversity(_httpContextAccessor.HttpContext!.User, universityUser.UniversityId))
            return Result.Failure<FacultyUserResponse>(_userErrors.Forbidden);

        if (_userManager.Users.Any(x => x.Email == request.Email && x.Id != request.UserId))
            return Result.Failure<FacultyUserResponse>(_userErrors.DuplicatedEmail);

        foreach (var role in request.Roles)
        {

            var roleEntity = await _roleManager.FindByNameAsync(role);

            if (roleEntity is null)
                return Result.Failure<FacultyUserResponse>(_roleErrors.NotFound);

            if (roleEntity.ScopeId != DefaultScopes.University.Id)
                return Result.Failure<FacultyUserResponse>(_roleErrors.ScopeIsNotValidForRole);
        }

        request.Adapt(universityUser.User);

        if (request.Password is not null)
        {
            universityUser.User.PasswordHash = _userManager.PasswordHasher.HashPassword(universityUser.User, request.Password);
        }

        var updateUserResult = await _userManager.UpdateAsync(universityUser.User);


        if (updateUserResult.Succeeded)
        {
            var currentRolesAsString = await _userManager.GetRolesAsync(universityUser.User);

            var newRolesAsString = request.Roles.Except(currentRolesAsString).ToList();

            var removedRolesAsString = currentRolesAsString.Except(request.Roles).ToList();

            if (newRolesAsString.Any())
            {
                var addResult = await _userManager.AddToRolesAsync(universityUser.User, newRolesAsString);
                if (!addResult.Succeeded)
                {
                    var err = addResult.Errors.First();
                    return Result.Failure(new Error(err.Code, err.Description, StatusCodes.Status400BadRequest));
                }
            }

            if (removedRolesAsString.Any())
            {
                var removeResult = await _userManager.RemoveFromRolesAsync(universityUser.User, removedRolesAsString);
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