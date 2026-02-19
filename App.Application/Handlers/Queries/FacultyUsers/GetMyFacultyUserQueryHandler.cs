using App.Application.Contracts.Responses.FacultyUsers;
using App.Application.Queries.Faculties;
using App.Application.Queries.FacultyUsers;
using App.Core.Extensions;

namespace App.Application.Handlers.Queries.FacultyUsers;

public class GetMyFacultyUserQueryHandler(
    UserErrors userErrors,
    IUnitOfWork unitOfWork,
    IHttpContextAccessor httpContextAccessor,
    UserManager<ApplicationUser> userManager) : IRequestHandler<GetMyFacultyUserQuery, Result<FacultyUserResponse>>
{
    private readonly UserErrors _userErrors = userErrors;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<Result<FacultyUserResponse>> Handle(GetMyFacultyUserQuery request, CancellationToken cancellationToken)
    {
        var facultyUser = await _unitOfWork.FacultyUsers.FindAsync(x => x.UserId == _httpContextAccessor.HttpContext!.User.GetUserId(), x => x.Include(u => u.User));

        if (facultyUser == null)
            return Result.Failure<FacultyUserResponse>(_userErrors.NotFound);

        var roles = await _userManager.GetRolesAsync(facultyUser.User);

        var response = facultyUser.User.Adapt<FacultyUserResponse>();

        response.Roles = [..roles];

        return Result.Success(response);
    }
}
