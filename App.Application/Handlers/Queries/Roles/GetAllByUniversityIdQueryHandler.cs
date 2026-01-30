using App.Application.Contracts.Responses.Roles;
using App.Application.Queries.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Handlers.Queries.Roles;

public class GetAllByUniversityIdQueryHandler : IRequestHandler<GetAllRolesByUniversityIdQuery, Result<List<RoleResponse>>>
{
    private readonly RoleManager<ApplicationRole> _roleManager;

    public GetAllByUniversityIdQueryHandler(RoleManager<ApplicationRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<Result<List<RoleResponse>>> Handle(GetAllRolesByUniversityIdQuery request, CancellationToken cancellationToken)
    {
        var roles = await _roleManager
                         .Roles
                         .Where(x => !x.IsDefualt && x.UniversityId==request.UniversityId && (!x.IsDeleted || (request.IncludeDisabled.HasValue && request.IncludeDisabled.Value)))
                         .ProjectToType<RoleResponse>().ToListAsync(cancellationToken);

        return Result.Success(roles);
    }
}
