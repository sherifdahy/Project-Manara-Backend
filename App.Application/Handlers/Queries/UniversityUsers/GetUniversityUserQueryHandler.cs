using App.Application.Contracts.Responses.FacultyUsers;
using App.Application.Queries.UniversityUsers;

namespace App.Application.Handlers.Queries.UniversityUsers;

public class GetUniversityUserQueryHandler(IUnitOfWork unitOfWork
    ,UserErrors userErrors
    ,IUniversityService universityService
    , IHttpContextAccessor httpContextAccessor
    ,UserManager<ApplicationUser> userManager) : IRequestHandler<GetUniversityUserQuery, Result<FacultyUserResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserErrors _userErrors = userErrors;
    private readonly IUniversityService _universityService = universityService;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly UserManager<ApplicationUser> _userManager = userManager;


    public async Task<Result<FacultyUserResponse>> Handle(GetUniversityUserQuery request, CancellationToken cancellationToken)
    {
        var universityUser = await _unitOfWork.UniversityUsers
            .FindAsync(x => x.UserId == request.Id, i => i.Include(p => p.User), cancellationToken);

        if (universityUser == null)
            return Result.Failure<FacultyUserResponse>(_userErrors.NotFound);

        if (!await _universityService.IsUserHasAccessToUniversity(_httpContextAccessor.HttpContext!.User, universityUser.UniversityId))
            return Result.Failure<FacultyUserResponse>(_userErrors.Forbidden);

        var roles = await _userManager.GetRolesAsync(universityUser.User);

        var response = universityUser.User.Adapt<FacultyUserResponse>();

        response.Roles = roles.ToList();

        return Result.Success(response);
    }
}
