using App.Application.Contracts.Responses.Scopes;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Queries.Scopes;

public record GetScopeQuery : IRequest<Result<ScopeDetailResponse>>
{
    public string Name { get; set; } = string.Empty;
}
