using App.Application.Contracts.Responses.Scopes;
using App.Application.Queries.Scopes;

namespace App.Application.Handlers.Queries.Scopes;

public class GetAllScopeQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllScopesQuery, Result<List<ScopeResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<List<ScopeResponse>>> Handle(GetAllScopesQuery request, CancellationToken cancellationToken)
    {
        var scopes = await _unitOfWork.Scopes.FindAllAsync(x=> true, [i=>i.ChildScopes.Where(r =>!r.IsDeleted),i=>i.ParentScope,i=>i.Roles.Where(r=> !r.IsDefault && !r.IsDeleted)], cancellationToken);

        var response = scopes.Adapt<List<ScopeResponse>>();

        return Result.Success(response);
    }
}
