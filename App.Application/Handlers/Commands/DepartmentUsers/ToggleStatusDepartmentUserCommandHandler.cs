using App.Application.Commands.DepartmentUsers;
using App.Application.Contracts.Responses.FacultyUsers;
using App.Core.Entities.Personnel;
using Microsoft.AspNetCore.Identity;

namespace App.Application.Handlers.Commands.DepartmentUsers;

public class ToggleStatusDepartmentUserCommandHandler(IUnitOfWork unitOfWork
    ,UserErrors userErrors
    ,IDepartmentService departmentService
    ,IHttpContextAccessor httpContextAccessor
    ,UserManager<ApplicationUser> userManager
    ) : IRequestHandler<ToggleStatusDepartmentUserCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserErrors _userErrors = userErrors;
    private readonly IDepartmentService _departmentService = departmentService;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly UserManager<ApplicationUser> _userManager = userManager;


    public async Task<Result> Handle(ToggleStatusDepartmentUserCommand request, CancellationToken cancellationToken)
    {
        var departmentUser = await _unitOfWork
            .DepartmentUsers.FindAsync(x => x.UserId == request.Id, i => i.Include(o => o.User), cancellationToken);

        if (departmentUser == null)
            return Result.Failure(_userErrors.NotFound);

        if (!await _departmentService.IsUserHasAccessToDepartment(_httpContextAccessor.HttpContext!.User, departmentUser.DepartmentId))
            return Result.Failure<FacultyUserResponse>(_userErrors.Forbidden);

        departmentUser.User.IsDeleted = !departmentUser.User.IsDeleted;

        var updateResult = await _userManager.UpdateAsync(departmentUser.User);

        return Result.Success();

    }
}
