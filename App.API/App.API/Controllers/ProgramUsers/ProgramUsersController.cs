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
        var query = new GetAllProgramUsersByProgramIdQuery() with { ProgramId = programId, IncludeDisabled = includeDisabled, Filters = filters };
        var result = await _mediator.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("/api/faculties/{facultyId}/[controller]")]
    [RequireFacultyAccess("facultyId")]
    [HasPermission(Permissions.GetProgramUsers)]
    public async Task<IActionResult> GetAll([FromRoute] int facultyId, [FromQuery] bool includeDisabled, [FromQuery] RequestFilters filters, CancellationToken cancellationToken)
    {
        var query = new GetAllProgramUserByFacultyIdQuery() with { FacultyId = facultyId, IncludeDisabled = includeDisabled, Filters = filters };
        var result = await _mediator.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("{id}")]
    [RequireUserAccess("id")]
    [HasPermission(Permissions.GetProgramUsers)]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
    {
        var query = new GetProgramUserQuery() with { Id = id };
        var result = await _mediator.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost("/api/faculties/{facultyId}/[controller]")]
    [RequireProgramAccess("facultyId")]
    [HasPermission(Permissions.CreateProgramUsers)]
    public async Task<IActionResult> Create([FromRoute] int facultyId, [FromBody] ProgramUserRequest request, CancellationToken cancellationToken)
    {
        var command = request.Adapt<CreateProgramUserCommand>() with { FacultyId = facultyId };
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? CreatedAtAction(nameof(Get), new { id = result.Value.Id }, result.Value) : result.ToProblem();
    }

    [HttpPut("{programUserId}")]
    [RequireUserAccess("programUserId")]
    [HasPermission(Permissions.UpdateProgramUsers)]
    public async Task<IActionResult> Update([FromRoute] int programUserId, [FromBody] UpdateProgramUserRequest request, CancellationToken cancellationToken)
    {
        var command = request.Adapt<UpdateProgramUserCommand>() with { UserId = programUserId };
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
