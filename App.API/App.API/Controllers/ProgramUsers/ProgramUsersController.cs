using App.API.Attributes;
using App.Application.Commands.DepartmentUsers;
using App.Application.Commands.ProgramUsers;
using App.Application.Contracts.Requests.DepartmentUsers;
using App.Application.Contracts.Requests.ProgramUsers;
using App.Application.Queries.DepartmentUsers;
using App.Application.Queries.ProgramUsers;
using App.Core.Extensions;
using App.Infrastructure.Abstractions.Consts;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers.ProgramUsers;

[Route("api/[controller]")]
[ApiController]
public class ProgramUsersController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;




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
}
