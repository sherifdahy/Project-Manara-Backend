using App.API.Attributes;
using App.Application.Abstractions;
using App.Application.Commands.DepartmentUsers;
using App.Application.Commands.ProgramUsers;
using App.Application.Contracts.Requests.ProgramUsers;
using App.Application.Queries.ProgramUsers;
using App.Core.Extensions;
using App.Infrastructure.Abstractions.Consts;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers.ProgramUsers;

[Route("api/[controller]")]
[ApiController]
public class ProgramUsersController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("/api/programs/{programId}/[controller]")]
    [RequireProgramAccess("programId")]
    [HasPermission(Permissions.GetProgramUsers)]
    public async Task<IActionResult> GetAll([FromQuery] bool includeDisabled, [FromQuery] RequestFilters filters, [FromRoute] int programId, CancellationToken cancellationToken)
    {
        var query = new GetAllProgramUsersQuery() with { ProgramId = programId, IncludeDisabled = includeDisabled, Filters = filters };
        var result = await _mediator.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }


    [HttpGet("{id}")]
    [RequireUserAccessAttribute("id")]
    [HasPermission(Permissions.GetProgramUsers)]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
    {
        var query = new GetProgramUserQuery() with { Id = id };
        var result = await _mediator.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost("/api/programs/{programId}/[controller]")]
    [RequireProgramAccess("programId")]
    [HasPermission(Permissions.CreateProgramUsers)]
    public async Task<IActionResult> Create([FromRoute] int programId, [FromBody] ProgramUserRequest request, CancellationToken cancellationToken)
    {
        var command = request.Adapt<CreateProgramUserCommand>() with { ProgramId = programId };
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? CreatedAtAction(nameof(Get), new { id = result.Value.Id }, result.Value) : result.ToProblem();
    }

    [HttpPut("{id}")]
    [RequireUserAccess("id")]
    [HasPermission(Permissions.UpdateProgramUsers)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ProgramUserRequest request, CancellationToken cancellationToken)
    {
        var command = request.Adapt<UpdateProgramUserCommand>() with { UserId = id };
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpDelete("{id}")]
    [RequireUserAccess("id")]
    [HasPermission(Permissions.ToggleStatusProgramUsers)]
    public async Task<IActionResult> ToggleStatus([FromRoute] int id, CancellationToken cancellationToken)
    {
        var command = new ToggleStatusProgramUserCommand() with { Id = id };
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
