using App.Application.Contracts.Responses.DepartmentUsers;
using App.Application.Queries.Instructors;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Handlers.Queries.Instructors;

public class GetAllInstructorsInsideFacultyQueryHandler : IRequestHandler<GetAllInstructorsInsideFacultyQuery, Result<PaginatedList<DepartmentUserResponse>>>
{
    public Task<Result<PaginatedList<DepartmentUserResponse>>> Handle(GetAllInstructorsInsideFacultyQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
