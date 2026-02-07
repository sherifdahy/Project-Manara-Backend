using App.Application.Commands.FacultyUsers;

namespace App.Application.Handlers.Commands.FacultyUsers;

public class ToggleStatusFacultyUserCommandHandler(UserManager<ApplicationUser> userManager,IUnitOfWork unitOfWork) : IRequestHandler<ToggleStatusFacultyUserCommand, Result>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(ToggleStatusFacultyUserCommand request, CancellationToken cancellationToken)
    {
        var facultyUser = await _unitOfWork.FacultyUsers.FindAsync(x => x.UserId == request.Id, [i => i.User], cancellationToken);

        if (facultyUser == null)
            return Result.Failure(UserErrors.NotFound);

        facultyUser.User.IsDeleted = !facultyUser.User.IsDeleted;

        var updateResult = await _userManager.UpdateAsync(facultyUser.User);

        return Result.Success();
    }
}
