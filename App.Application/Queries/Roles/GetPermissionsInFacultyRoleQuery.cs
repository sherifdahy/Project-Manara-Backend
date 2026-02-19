using App.Application.Contracts.Responses.Roles;

namespace App.Application.Queries.Roles;

public record GetPermissionsInFacultyRoleQuery : IRequest<Result<GetPermissionsInFacultyRoleResponse>>
{
    public int RoleId { get; set; }
    public int FacultyId { get; set; }

    public GetPermissionsInFacultyRoleQuery(int roleId,int facultyId)
    {
        RoleId = roleId;
        FacultyId = facultyId;
    }
}
