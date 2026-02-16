using App.API.Attributes;
using App.Application.Abstractions;
using App.Application.Commands.DepartmentUsers;
using App.Application.Commands.FacultyUsers;
using App.Application.Contracts.Requests.DepartmentUsers;
using App.Application.Contracts.Requests.FacultyUsers;
using App.Application.Queries.DepartmentUsers;
using App.Application.Queries.FacultyUsers;
using App.Core.Extensions;
using App.Infrastructure.Abstractions.Consts;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers.DepartmentUsers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class DepartmentUsersController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("/api/departments/{departmentId}/[controller]")]
    [RequireDepartmentAccess("departmentId")]
    [HasPermission(Permissions.GetDepartmentUsers)]
    public async Task<IActionResult> GetAll([FromQuery] bool includeDisabled, [FromQuery] RequestFilters filters, [FromRoute] int departmentId, CancellationToken cancellationToken)
    {
        var query = new GetAllDepartmentUsersQuery() with { DepartmentId = departmentId, IncludeDisabled = includeDisabled, Filters = filters };
        var result = await _mediator.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("{id}")]
    [RequireUserAccessAttribute("id")]
    [HasPermission(Permissions.GetDepartmentUsers)]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
    {
        var query = new GetDepartmentUserQuery() with { Id = id };
        var result = await _mediator.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost("/api/departments/{departmentId}/[controller]")]
    [RequireDepartmentAccess("departmentId")]
    [HasPermission(Permissions.CreateDepartmentUsers)]
    public async Task<IActionResult> Create([FromRoute] int departmentId, [FromBody] DepartmentUserRequest request, CancellationToken cancellationToken)
    {
        var command = request.Adapt<CreateDepartmentUserCommand>() with { DepartmentId = departmentId };
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? CreatedAtAction(nameof(Get), new { id = result.Value.Id }, result.Value) : result.ToProblem();
    }

    [HttpPut("{id}")]
    [RequireUserAccess("id")]
    [HasPermission(Permissions.UpdateDepartmentUsers)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] DepartmentUserRequest request, CancellationToken cancellationToken)
    {
        var command = request.Adapt<UpdateDepartmentUserCommand>() with { UserId = id };
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpDelete("{id}")]
    [RequireUserAccess("id")]
    [HasPermission(Permissions.ToggleStatusDepartmentUsers)]
    public async Task<IActionResult> ToggleStatus([FromRoute] int id, CancellationToken cancellationToken)
    {
        var command = new ToggleStatusDepartmentUserCommand() with { Id = id };
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
