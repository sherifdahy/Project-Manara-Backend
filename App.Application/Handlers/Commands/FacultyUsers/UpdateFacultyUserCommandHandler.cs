using App.Application.Commands.FacultyUsers;
using App.Application.Contracts.Responses.FacultyUsers;
using App.Core.Consts;
using App.Services;
using System.Data;

namespace App.Application.Handlers.Commands.FacultyUsers;

public class UpdateFacultyUserCommandHandler(UserErrors userErrors,RoleErrors roleErrors,IUnitOfWork unitOfWork,IFacultyService facultyService,IHttpContextAccessor httpContextAccessor,UserManager<ApplicationUser> userManager,RoleManager<ApplicationRole> roleManager) : IRequestHandler<UpdateFacultyUserCommand, Result>
{
    private readonly UserErrors _userErrors = userErrors;
    private readonly RoleErrors _roleErrors = roleErrors;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IFacultyService _facultyService = facultyService;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;

    public async Task<Result> Handle(UpdateFacultyUserCommand request, CancellationToken cancellationToken)
    {
        var facultyUser = await _unitOfWork.FacultyUsers.FindAsync(x => x.UserId == request.UserId,i=>i.Include(p=>p.User),cancellationToken);

        if (facultyUser == null)
            return Result.Failure(_userErrors.NotFound);

        if (!await _facultyService.IsUserHasAccessToFaculty(_httpContextAccessor.HttpContext!.User, facultyUser.FacultyId))
            return Result.Failure<FacultyUserResponse>(_userErrors.Forbidden);

        if (_userManager.Users.Any(x => x.Email == request.Email && x.Id != request.UserId))
            return Result.Failure<FacultyUserResponse>(_userErrors.DuplicatedEmail);

        foreach (var role in request.Roles)
        {

            var roleEntity = await _roleManager.FindByNameAsync(role);

            if (roleEntity is null)
                return Result.Failure<FacultyUserResponse>(_roleErrors.NotFound);

            if (roleEntity.ScopeId != DefaultScopes.Faculty.Id)
                return Result.Failure<FacultyUserResponse>(_roleErrors.ScopeIsNotValidForRole);
        }


        request.Adapt(facultyUser.User);
        
        var updateUserResult =  await _userManager.UpdateAsync(facultyUser.User);

        if(updateUserResult.Succeeded)
        {
            var currentRolesAsString = await _userManager.GetRolesAsync(facultyUser.User);

            var newRolesAsString = request.Roles.Except(currentRolesAsString).ToList();

            var removedRolesAsString = currentRolesAsString.Except(request.Roles).ToList();

            if (newRolesAsString.Any())
            {
                var addResult = await _userManager.AddToRolesAsync(facultyUser.User, newRolesAsString);
                if (!addResult.Succeeded)
                {
                    var err = addResult.Errors.First();
                    return Result.Failure(new Error(err.Code, err.Description, StatusCodes.Status400BadRequest));
                }
            }

            if (removedRolesAsString.Any())
            {
                var removeResult = await _userManager.RemoveFromRolesAsync(facultyUser.User, removedRolesAsString);
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
