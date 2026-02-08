using App.Application.Commands.FacultyUsers;
using App.Application.Contracts.Responses.FacultyUsers;
using System.Data;

namespace App.Application.Handlers.Commands.FacultyUsers;

public class UpdateFacultyUserCommandHandler(RoleErrors roleErrors,IUnitOfWork unitOfWork,UserManager<ApplicationUser> userManager,RoleManager<ApplicationRole> roleManager) : IRequestHandler<UpdateFacultyUserCommand, Result>
{
    private readonly RoleErrors _roleErrors = roleErrors;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;

    public async Task<Result> Handle(UpdateFacultyUserCommand request, CancellationToken cancellationToken)
    {
        var facultyUser = await _unitOfWork.FacultyUsers.FindAsync(x => x.UserId == request.UserId,[i=>i.User],cancellationToken);

        if (facultyUser == null)
            return Result.Failure(UserErrors.NotFound);

        if (_userManager.Users.Any(x => x.Email == request.Email && x.Id != request.UserId))
            return Result.Failure<FacultyUserResponse>(UserErrors.DuplicatedEmail);

        foreach (var role in request.Roles)
        {
            if (!await _roleManager.RoleExistsAsync(role))
                return Result.Failure<FacultyUserResponse>(_roleErrors.NotFound);
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
