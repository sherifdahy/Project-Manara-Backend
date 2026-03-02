using App.Application.Contracts.Responses.DepartmentUsers;
using App.Application.Queries.DepartmentUsers;
using App.Core.Entities.Personnel;
using System.Linq.Expressions;

namespace App.Application.Handlers.Queries.DepartmentUsers;

public class GetAllDepartmentUsersQueryHandler(IUnitOfWork unitOfWork
    ,DepartmentErrors departmentErrors
    ,UserManager<ApplicationUser> userManager) : IRequestHandler<GetAllDepartmentUsersQuery, Result<PaginatedList<DepartmentUserResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly DepartmentErrors _departmentErrors = departmentErrors;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    public async Task<Result<PaginatedList<DepartmentUserResponse>>> Handle(GetAllDepartmentUsersQuery request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.Departments.GetByIdAsync(request.DepartmentId) is null)
            return Result.Failure<PaginatedList<DepartmentUserResponse>>(_departmentErrors.NotFound);

        Expression<Func<DepartmentUser, bool>> query =
            x => x.DepartmentId == request.DepartmentId &&
                (string.IsNullOrEmpty(request.Filters.SearchValue) 
                || x.User.Name.Contains(request.Filters.SearchValue)
                || x.User.Email!.Contains(request.Filters.SearchValue) 
                || x.User.NationalId.Contains(request.Filters.SearchValue)) &&
                (request.IncludeDisabled == true || x.User.IsDeleted == false);

        var count = await _unitOfWork.DepartmentUsers.CountAsync(query);

        var departmentUsers = await _unitOfWork.DepartmentUsers.FindAllAsync(
            query,
            i => i.Include(d => d.User),
            (request.Filters.PageNumber - 1) * request.Filters.PageSize,
            request.Filters.PageSize,
            request.Filters.SortColumn,
            request.Filters.SortDirection,
            cancellationToken);

        var response = new List<DepartmentUserResponse>();

        foreach (var x in departmentUsers)
        {
            var roles = (await _userManager.GetRolesAsync(x.User)).ToList();

            response.Add(new DepartmentUserResponse
            {
                Id = x.UserId,
                Email = x.User.Email!,
                Name = x.User.Name,
                NationalId = x.User.NationalId,
                Roles = roles,
                IsDeleted = x.User.IsDeleted,
                IsDisabled = x.User.IsDisabled,
            });
        }

        return Result.Success(PaginatedList<DepartmentUserResponse>.Create(response, count, request.Filters.PageNumber, request.Filters.PageSize));


    }
}