using App.Application.Commands.FacultyUsers;
using App.Application.Contracts.Responses.FacultyUsers;
using App.Services;

namespace App.Application.Handlers.Commands.FacultyUsers;

public class ToggleStatusFacultyUserCommandHandler(UserErrors userErrors
    ,IHttpContextAccessor httpContextAccessor
    ,IFacultyService facultyService
    ,UserManager<ApplicationUser> userManager
    ,IUnitOfWork unitOfWork) : IRequestHandler<ToggleStatusFacultyUserCommand, Result>
{
    private readonly UserErrors _userErrors = userErrors;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IFacultyService _facultyService = facultyService;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(ToggleStatusFacultyUserCommand request, CancellationToken cancellationToken)
    {
        var facultyUser = await _unitOfWork.FacultyUsers.FindAsync(x => x.UserId == request.Id, [i => i.User], cancellationToken);

        if (facultyUser == null)
            return Result.Failure(_userErrors.NotFound);

        if (!await _facultyService.IsUserHasAccessToFaculty(_httpContextAccessor.HttpContext!.User, facultyUser.FacultyId))
            return Result.Failure<FacultyUserResponse>(_userErrors.Forbidden);

        facultyUser.User.IsDeleted = !facultyUser.User.IsDeleted;

        var updateResult = await _userManager.UpdateAsync(facultyUser.User);

        return Result.Success();
    }
}
