using App.Application.Contracts.Responses.Scopes;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Queries.Scopes;

public record GetAllScopesQuery : IRequest<Result<List<ScopeResponse>>>
{

}
