using App.Application.Contracts.Responses.DepartmentUsers;
using App.Application.Queries.DepartmentUsers;

namespace App.Application.Handlers.Queries.DepartmentUsers;

public class GetDepartmentUserQueryHandler(IUnitOfWork unitOfWork
    ,UserErrors userErrors
    ,IDepartmentService departmentService
    ,IHttpContextAccessor httpContextAccessor
    ,UserManager<ApplicationUser> userManager) : IRequestHandler<GetDepartmentUserQuery, Result<DepartmentUserResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserErrors _userErrors = userErrors;
    private readonly IDepartmentService _departmentService = departmentService;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<Result<DepartmentUserResponse>> Handle(GetDepartmentUserQuery request, CancellationToken cancellationToken)
    {
        var departmentUser = await _unitOfWork
            .DepartmentUsers.FindAsync(x => x.UserId == request.Id, i => i.Include(d => d.User), cancellationToken);

        if (departmentUser == null)
            return Result.Failure<DepartmentUserResponse>(_userErrors.NotFound);

        if (!await _departmentService.IsUserHasAccessToDepartment(_httpContextAccessor.HttpContext!.User, departmentUser.DepartmentId))
            return Result.Failure<DepartmentUserResponse>(_userErrors.Forbidden);

        var roles = await _userManager.GetRolesAsync(departmentUser.User);

        var response = departmentUser.User.Adapt<DepartmentUserResponse>();

        response.Roles = roles.ToList();

        return Result.Success(response);

    }
}
