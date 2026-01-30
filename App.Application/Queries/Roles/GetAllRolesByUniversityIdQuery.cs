using App.Application.Contracts.Responses.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Queries.Roles;

public class GetAllRolesByUniversityIdQuery : IRequest<Result<List<RoleResponse>>>
{
    public bool? IncludeDisabled { get; set; } = false;
    public int UniversityId { get; set; } 

    public GetAllRolesByUniversityIdQuery(bool? includeDisabled, int universityId)
    {
        IncludeDisabled = includeDisabled;
        UniversityId = universityId;
    }
}
