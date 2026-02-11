using App.API.Attributes;
using App.Application.Queries.Scopes;
using App.Core.Extensions;
using App.Infrastructure.Abstractions.Consts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers.Scopes;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ScopesController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    [HasPermission(Permissions.GetScopes)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var query = new GetAllScopesQuery();
        var result = await _mediator.Send(query,cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("{scopeName}")]
    [RequireScopeAccess("scopeName")]
    [HasPermission(Permissions.GetScopeDetail)]
    public async Task<IActionResult> Get([FromRoute] string scopeName,CancellationToken cancellationToken = default)
    {
        var query = new GetScopeQuery() with { Name = scopeName };
        var result = await _mediator.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
}
