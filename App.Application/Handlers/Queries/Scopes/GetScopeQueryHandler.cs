using App.Application.Contracts.Responses.Roles;
using App.Application.Contracts.Responses.Scopes;
using App.Application.Queries.Scopes;

namespace App.Application.Handlers.Queries.Scopes;

public class GetScopeQueryHandler(IUnitOfWork unitOfWork,ScopeErrors scopeErrors) : IRequestHandler<GetScopeQuery, Result<ScopeDetailResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ScopeErrors _scopeErrors = scopeErrors;

    public async Task<Result<ScopeDetailResponse>> Handle(GetScopeQuery request, CancellationToken cancellationToken)
    {
        var scope = await _unitOfWork.Scopes.FindAsync(x=>x.Name == request.Name, 
            i=>i.Include(d=>d.ParentScope)
                .Include(d=>d.Roles.Where(e=> !e.IsDefault && !e.IsDeleted))
                .Include(o=>o.ChildScopes),cancellationToken);

        if (scope == null)
            return Result.Failure<ScopeDetailResponse>(_scopeErrors.NotFound);

        var response = scope.Adapt<ScopeDetailResponse>();

        return Result.Success(response);
    }
}
