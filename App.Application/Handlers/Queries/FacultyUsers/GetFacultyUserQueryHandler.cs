using App.Application.Contracts.Responses.FacultyUsers;
using App.Application.Queries.FacultyUsers;
using App.Services;

namespace App.Application.Handlers.Queries.FacultyUsers;

public class GetFacultyUserQueryHandler(UserErrors userErrors,IHttpContextAccessor httpContextAccessor,IUnitOfWork unitOfWork,IFacultyService facultyService,UserManager<ApplicationUser> userManager) : IRequestHandler<GetFacultyUserQuery, Result<FacultyUserResponse>>
{
    private readonly UserErrors _userErrors = userErrors;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IFacultyService _facultyService = facultyService;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    
    public async Task<Result<FacultyUserResponse>> Handle(GetFacultyUserQuery request, CancellationToken cancellationToken)
    {
        var facultyUser = await _unitOfWork.FacultyUsers.FindAsync(x => x.UserId == request.Id, [i => i.User], cancellationToken);

        if (facultyUser == null)
            return Result.Failure<FacultyUserResponse>(_userErrors.NotFound);


        if (!await _facultyService.IsUserHasAccessToFaculty(_httpContextAccessor.HttpContext!.User, facultyUser.FacultyId))
            return Result.Failure<FacultyUserResponse>(_userErrors.Forbidden);

        var roles = await _userManager.GetRolesAsync(facultyUser.User);

        var response = facultyUser.User.Adapt<FacultyUserResponse>();

        response.Roles = roles.ToList();

        return Result.Success(response);
    }
}
