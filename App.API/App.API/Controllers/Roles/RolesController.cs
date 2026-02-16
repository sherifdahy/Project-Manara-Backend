using App.API.Attributes;
using App.Application.Commands.Roles;
using App.Application.Contracts.Requests.Roles;
using App.Application.Queries.Permissions;
using App.Application.Queries.Roles;
using App.Core.Extensions;
using App.Infrastructure.Abstractions.Consts;
using Mapster;
using MediatR;
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
        var result = await _mediator.Send(new GetAllRolesQuery(includeDisabled), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("{id}")]
    [RequireRoleAccess("id")]
    [HasPermission(Permissions.GetRoles)]
    public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetRoleByIdQuery(id), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("/api/faculties/{facultyId}/roles/{roleId}")]
    [RequireFacultyAccess("facultyId")]
    [RequireRoleAccess("roleId")]
    [HasPermission(Permissions.GetRoles)]
    public async Task<IActionResult> GetPermissionsInFacultyRole([FromRoute] int facultyId, [FromRoute] int roleId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetPermissionsInFacultyRoleQuery(roleId, facultyId), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }


    [HttpPost]
    [HasPermission(Permissions.CreateRoles)]
    public async Task<IActionResult> Create([FromBody] CreateRoleRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request.Adapt<CreateRoleCommand>(), cancellationToken);
        return result.IsSuccess ? CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value) : result.ToProblem();
    }

    [HttpPut("{id}")]
    [RequireRoleAccess("id")]
    [HasPermission(Permissions.UpdateRoles)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateRoleRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request.Adapt<UpdateRoleCommand>() with { Id = id }, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpDelete("{id}")]
    [RequireRoleAccess("id")]
    [HasPermission(Permissions.ToggleStatusRoles)]
    public async Task<IActionResult> ToggleStatus([FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new ToggleStatusRoleCommand(id), cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }


}
