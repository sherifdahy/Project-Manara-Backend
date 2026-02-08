using App.Application.Contracts.Responses.Roles;

namespace App.Application.Queries.Roles;

public class GetRoleInFacultyQuery : IRequest<Result<GetRoleInFacultyResponse>>
{
    public int RoleId { get; set; }
    public int FacultyId { get; set; }

    public GetRoleInFacultyQuery(int roleId,int facultyId)
    {
        RoleId = roleId;
        FacultyId = facultyId;
    }
}
