using App.API.Attributes;
using App.Application.Commands.FacultyUsers;
using App.Application.Commands.UniversityUsers;
using App.Application.Contracts.Requests.FacultyUsers;
using App.Application.Contracts.Requests.UniversityUsers;
using App.Application.Queries.FacultyUsers;
using App.Application.Queries.UniversityUsers;
using App.Core.Extensions;
using App.Infrastructure.Abstractions.Consts;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers.UniversityUsers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UniversityUsersController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("/api/universities/{universityId}/[controller]")]
    [RequireUniversityAccess("universityId")]
    [HasPermission(Permissions.GetUniversityUsers)]
    public async Task<IActionResult> GetAll([FromRoute] int universityId, CancellationToken cancellationToken)
    {
        var query = new GetAllUniversityUsersQuery() with { UniversityId=universityId };
        var result = await _mediator.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }


    [HttpGet("{id}")]
    [RequireUserAccessAttribute("id")]
    [HasPermission(Permissions.GetUniversityUsers)]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
    {
        var query = new GetUniversityUserQuery() with { Id = id };
        var result = await _mediator.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost("/api/universities/{universityId}/[controller]")]
    [RequireUniversityAccess("universityId")]
    [HasPermission(Permissions.CreateUniversityUsers)]
    public async Task<IActionResult> Create([FromRoute] int universityId, [FromBody] UniversityUserRequest request, CancellationToken cancellationToken)
    {
        var command = request.Adapt<CreateUniversityUserCommand>() with { UniversityId = universityId };
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? CreatedAtAction(nameof(Get), new { id = result.Value.Id }, result.Value) : result.ToProblem();
    }

    [HttpPut("{id}")]
    [RequireUserAccess("id")]
    [HasPermission(Permissions.UpdateUniversityUsers)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UniversityUserRequest request, CancellationToken cancellationToken)
    {
        var command = request.Adapt<UpdateUniversityUserCommand>() with { UserId = id };
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpDelete("{id}")]
    [RequireUserAccess("id")]
    [HasPermission(Permissions.ToggleStatusUniversityUsers)]
    public async Task<IActionResult> ToggleStatus([FromRoute] int id, CancellationToken cancellationToken)
    {
        var command = new ToggleStatusUniversityUserCommand() with { Id = id };
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
