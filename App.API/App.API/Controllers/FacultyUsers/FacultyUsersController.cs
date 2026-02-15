using App.API.Attributes;
using App.Application.Abstractions;
using App.Application.Commands.FacultyUsers;
using App.Application.Contracts.Requests.FacultyUsers;
using App.Application.Queries.FacultyUsers;
using App.Core.Extensions;
using App.Infrastructure.Abstractions.Consts;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.API.Controllers.FacultyUsers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class FacultyUsersController(IMediator mediator,IRoleService roleService) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly IRoleService _roleService = roleService;

    [HttpGet("/api/faculties/{facultyId}/[controller]")]
    [RequireFacultyAccess("facultyId")]
    [HasPermission(Permissions.GetFacultyUsers)]
    public async Task<IActionResult> GetAll([FromQuery] bool includeDisabled,[FromQuery] RequestFilters filters,[FromRoute] int facultyId, CancellationToken cancellationToken)
    {
        var query = new GetAllFacultyUsersQuery() with { FacultyId = facultyId ,IncludeDisabled = includeDisabled,Filters = filters};
        var result = await _mediator.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("{id}")]
    [RequireUserAccessAttribute("id")]
    [HasPermission(Permissions.GetFacultyUsers)]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
    {
        var query = new GetFacultyUserQuery() with { Id = id };
        var result = await _mediator.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost("/api/faculties/{facultyId}/[controller]")]
    [RequireFacultyAccess("facultyId")]
    [HasPermission(Permissions.CreateFacultyUsers)]
    public async Task<IActionResult> Create([FromRoute] int facultyId, [FromBody] FacultyUserRequest request, CancellationToken cancellationToken)
    {
        var command = request.Adapt<CreateFacultyUserCommand>() with { FacultyId = facultyId };
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? Created() : result.ToProblem();
    }

    [HttpPut("{id}")]
    [RequireUserAccess("id")]
    [HasPermission(Permissions.UpdateFacultyUsers)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] FacultyUserRequest request,CancellationToken cancellationToken)
    {
        var command = request.Adapt<UpdateFacultyUserCommand>() with { UserId = id };
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpDelete("{id}")]
    [RequireUserAccess("id")]
    [HasPermission(Permissions.ToggleStatusFacultyUsers)]
    public async Task<IActionResult> ToggleStatus([FromRoute] int id, CancellationToken cancellationToken)
    {
        var command = new ToggleStatusFacultyUserCommand () with { Id = id };
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }



}
