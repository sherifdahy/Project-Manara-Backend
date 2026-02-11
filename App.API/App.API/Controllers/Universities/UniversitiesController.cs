using App.Application.Commands.Universities;
using App.Application.Contracts.Requests.Universities;
using App.Application.Queries.Roles;
using App.Application.Queries.Universities;
using App.Core.Extensions;
using App.Infrastructure.Abstractions.Consts;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers.Universities;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UniversitiesController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    [HasPermission(Permissions.GetUniversities)]
    public async Task<IActionResult> GetAll([FromQuery] bool includeDisabled = false, CancellationToken cancellationToken = default)
    {
        var query = new GetAllUniverisitiesQuery(includeDisabled);
        var result = await _mediator.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("{id:int}")]
    [RequireUniversityAccess("id")]
    [HasPermission(Permissions.GetUniversities)]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken = default)
    {
        var query = new GetUniversityQuery(id);
        var result = await _mediator.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("my")]
    [RequireUniversityAccess("id")]
    [HasPermission(Permissions.GetUniversities)]
    public async Task<IActionResult> My(int id, CancellationToken cancellationToken = default)
    {
        var query = new GetMyUniversityQuery();
        var result = await _mediator.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost]
    [HasPermission(Permissions.CreateUniversities)]
    public async Task<IActionResult> Create(UniversityRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request.Adapt<CreateUniversityCommand>(), cancellationToken);
        return result.IsSuccess ? CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value) : result.ToProblem();
    }
    [HttpPut("{id}")]
    [RequireUniversityAccess("id")]
    [HasPermission(Permissions.UpdateUniversities)]
    public async Task<IActionResult> Update([FromRoute]int id,[FromBody]UniversityRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request.Adapt<UpdateUniversityCommand>() with { Id = id }, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpDelete("{id}/toggle-status")]
    [RequireUniversityAccess("id")]
    [HasPermission(Permissions.ToggleStatusUniversities)]
    public async Task<IActionResult> ToggleStatus(int id, CancellationToken cancellationToken = default)
    {
        var command = new ToggleStatusUniveristyCommand(id);
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

}
