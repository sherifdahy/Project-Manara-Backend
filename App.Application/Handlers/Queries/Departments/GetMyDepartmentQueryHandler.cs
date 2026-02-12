using App.Application.Contracts.Responses.Departments;
using App.Application.Queries.Departments;
using App.Core.Extensions;

namespace App.Application.Handlers.Queries.Departments;

public class GetMyDepartmentQueryHandler(IUnitOfWork unitOfWork
    , IHttpContextAccessor httpContextAccessor
    ,DepartmentErrors departmentErrors) : IRequestHandler<GetMyDepartmentQuery, Result<DepartmentDetailResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor = httpContextAccessor;
    private readonly DepartmentErrors _departmentErrors = departmentErrors;

    public async Task<Result<DepartmentDetailResponse>> Handle(GetMyDepartmentQuery request, CancellationToken cancellationToken)
    {
        var userId = _contextAccessor.HttpContext!.User.GetUserId();

        var departmentUser = await _unitOfWork.DepartmentUsers.FindAsync(x => x.UserId == userId, [i => i.Department], cancellationToken);

        if (departmentUser == null)
            return Result.Failure<DepartmentDetailResponse>(_departmentErrors.NotFoundForCurrentUser);

        var response = departmentUser.Department.Adapt<DepartmentDetailResponse>();

        return Result.Success(response);

    }
}
