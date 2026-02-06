using App.Application.Queries.Permissions;
using App.Infrastructure.Abstractions.Consts;

namespace App.Application.Handlers.Queries.Permissions;

public class GetAllPermissionsQueryHandler : IRequestHandler<GetAllPermissionsQuery, Result<List<string>>>
{
    public  Task<Result<List<string>>> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
    {
       var permissions = Infrastructure.Abstractions.Consts.Permissions.GetAllPermissions().ToList();

        return Task.FromResult(Result.Success<List<string>>(permissions));
    }
}
