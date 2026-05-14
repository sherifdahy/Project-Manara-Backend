using App.Application.Contracts.Responses.DepartmentUsers;
using App.Application.Queries.DepartmentUsers;
using App.Infrastructure.Abstractions.Consts;
using App.Infrastructure.Presistance.Data;
using System.Linq.Dynamic.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Handlers.Queries.DepartmentUsers;

public class GetInstructorsQueryHandler(ApplicationDbContext context,DepartmentErrors departmentErrors) : IRequestHandler<GetInstructorsQuery, Result<PaginatedList<DepartmentUserResponse>>>
{
    private readonly ApplicationDbContext _context = context;
    private readonly DepartmentErrors _departmentErrors = departmentErrors;

    public async Task<Result<PaginatedList<DepartmentUserResponse>>> Handle(GetInstructorsQuery request, CancellationToken cancellationToken)
    {
        var query =
            from departmentUser in _context.DepartmentUsers
                .AsNoTracking()
                .Include(x => x.User)
            join userRole in _context.UserRoles
                on departmentUser.UserId equals userRole.UserId
            where userRole.RoleId == DefaultRoles.InstructorRoleId
                  && departmentUser.DepartmentId == request.DepartmentId
                  && (string.IsNullOrEmpty(request.filters.SearchValue) ? true : departmentUser.User.Name.Contains(request.filters.SearchValue!) || departmentUser.User.NationalId.Contains(request.filters.SearchValue!))
            select departmentUser;

        var count = await query.CountAsync(cancellationToken);

        if (!string.IsNullOrEmpty(request.filters.SortColumn) && !string.IsNullOrEmpty(request.filters.SortDirection))
        {
            query = query.OrderBy(
                $"User.{request.filters.SortColumn} {request.filters.SortDirection}");
        }

        var data = await query
            .Skip((request.filters.PageNumber - 1) * request.filters.PageSize)
            .Take(request.filters.PageSize)
            .Select(x => x.User.Adapt<DepartmentUserResponse>())
            .ToListAsync(cancellationToken);

        return Result.Success(
            PaginatedList<DepartmentUserResponse>.Create(
                data,
                count,
                request.filters.PageNumber,
                request.filters.PageSize));
    }
}
