using App.Application.Authentication.Filters;
using App.Application.Commands.Roles;
using App.Application.Contracts.Requests.Roles;
using App.Application.Queries.Roles;
using App.Core.Extensions;
using App.Infrastructure.Abstractions.Consts;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers.Roles;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RolesController(IMediator _mediator) : ControllerBase
{

    [HttpGet]
    [HasPermission(Permissions.GetRoles)]
    public async Task<IActionResult> GetAllRoles([FromQuery] bool includeDisabled, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllRolesCommand(includeDisabled), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("{id}")]
    [HasPermission(Permissions.GetRoles)]
    public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetRoleByIdCommand(id), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost("/api/universities/{universityId:int}/roles")]
    [HasPermission(Permissions.CreateRoles)]
    public async Task<IActionResult> Create([FromRoute] int universityId,[FromBody] RoleRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request.Adapt<CreateRoleCommand>() with { UniversityId=universityId}, cancellationToken);
        return result.IsSuccess ? CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value) : result.ToProblem();
    }

    [HttpPut("{id}")]
    [HasPermission(Permissions.UpdateRoles)]
    public async Task<IActionResult> Update([FromRoute] int id,[FromBody] RoleRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request.Adapt<UpdateRoleCommand>() with {Id=id}, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpDelete("{id}")]
    [HasPermission(Permissions.ToggleStatusRoles)]
    public async Task<IActionResult> ToggleStatus([FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new ToggleStatusRoleCommand(id), cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }




}
