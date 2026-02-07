using App.Application.Contracts.Responses.FacultyUsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Queries.FacultyUsers;

public record GetAllFacultyUsersQuery : IRequest<Result<List<FacultyUserResponse>>>
{
    public int FacultyId { get; set; }
}
