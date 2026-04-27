using App.Application.Contracts.Responses.DepartmentUsers;
using App.Application.Queries.Doctors;
using App.Infrastructure.Abstractions.Consts;
using App.Infrastructure.Presistance.Data;

namespace App.Application.Handlers.Queries.Doctors;

public class GetAllDoctorsInsideFacultyQueryHandler : IRequestHandler<GetAllDoctorsInsideFacultyQuery, Result<PaginatedList<DepartmentUserResponse>>>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly UserErrors _userErrors;

    public GetAllDoctorsInsideFacultyQueryHandler(ApplicationDbContext dbContext,UserErrors userErrors)
    {
        _dbContext = dbContext;
        _userErrors = userErrors;
    }

    public async Task<Result<PaginatedList<DepartmentUserResponse>>> Handle(GetAllDoctorsInsideFacultyQuery request,CancellationToken cancellationToken)
    {
        if (!await _dbContext.Faculties.AnyAsync(x => x.Id == request.FacultyId))
            return Result.Failure<PaginatedList<DepartmentUserResponse>>(_userErrors.NotFound);

        var query =
            from du in _dbContext.DepartmentUsers
            join ur in _dbContext.UserRoles on du.UserId equals ur.UserId
            join r in _dbContext.Roles on ur.RoleId equals r.Id
            where du.Department.FacultyId == request.FacultyId
                  && r.Name == DefaultRoles.Doctor
            select du;

        var count = await query.CountAsync(cancellationToken);

        var data = await query
            .Include(x => x.User)
            .Skip((request.Filters.PageNumber -1) * request.Filters.PageSize)
            .Take(request.Filters.PageSize)
            .ToListAsync(cancellationToken);

        var mapped = data.Select(x=> x.User).Adapt<List<DepartmentUserResponse>>();

        return Result.Success(PaginatedList<DepartmentUserResponse>.Create(mapped, count,request.Filters.PageNumber,request.Filters.PageSize));
    }
}