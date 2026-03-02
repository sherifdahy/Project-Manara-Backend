using App.Application.Contracts.Responses.UniversityUser;
using App.Application.Queries.UniversityUsers;
using App.Core.Entities.Personnel;
using System.Linq.Expressions;

namespace App.Application.Handlers.Queries.UniversityUsers;

public class GetAllUniversityUsersQueryHandler(
    IUnitOfWork unitOfWork,
    UniversityErrors universityErrors,
    UserManager<ApplicationUser> userManager) : IRequestHandler<GetAllUniversityUsersQuery, Result<PaginatedList<UniversityUserResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UniversityErrors _universityErrors = universityErrors;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<Result<PaginatedList<UniversityUserResponse>>> Handle(GetAllUniversityUsersQuery request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.Universities.GetByIdAsync(request.UniversityId) is null)
            return Result.Failure<PaginatedList<UniversityUserResponse>>(_universityErrors.NotFound);

        Expression<Func<UniversityUser, bool>> query =
            x => x.UniversityId == request.UniversityId &&
            (string.IsNullOrEmpty(request.Filters.SearchValue) || x.User.Name.Contains(request.Filters.SearchValue) || x.User.Email!.Contains(request.Filters.SearchValue) || x.User.NationalId.Contains(request.Filters.SearchValue)) &&
            (request.IncludeDisabled == false || x.User.IsDeleted == false);

        var count = await _unitOfWork.UniversityUsers.CountAsync(query);

        var universityUsers = await _unitOfWork.UniversityUsers.FindAllAsync(
            query,
            i => i.Include(d => d.User),
            (request.Filters.PageNumber - 1) * request.Filters.PageSize,
            request.Filters.PageSize,
            request.Filters.SortColumn,
            request.Filters.SortDirection,
            cancellationToken);

        var response = new List<UniversityUserResponse>();

        foreach (var x in universityUsers)
        {
            var roles = (await _userManager.GetRolesAsync(x.User)).ToList();
            
            var temp = x.User.Adapt<UniversityUserResponse>();
            
            temp.Roles = roles;
            
            response.Add(temp);
        }

        return Result.Success(PaginatedList<UniversityUserResponse>.Create(response, count, request.Filters.PageNumber, request.Filters.PageSize));

    }
}
