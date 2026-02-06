namespace App.Application.Queries.Permissions;

public class GetAllPermissionsQuery : IRequest<Result<List<string>>>
{
    public GetAllPermissionsQuery()
    {
    }
}
