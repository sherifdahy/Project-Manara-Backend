using App.API.Attributes;
using App.Application.Abstractions;
using App.Application.Commands.Enrollments;
using App.Application.Contracts.Requests.Enrollments;
using App.Application.Queries.Enrollments;
using App.Core.Extensions;
using App.Infrastructure.Abstractions.Consts;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers.Enrollments;

[Route("api/[controller]")]
[ApiController]
public class EnrollmentsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("/api/programs/{programId}/enrollments")]
    [RequireProgramAccess("programId")]
    [HasPermission(Permissions.GetEnrollments)]
    public async Task<IActionResult> GetAllInProgram 
        (int programId,[FromQuery] bool includeDisabled, [FromQuery] RequestFilters filters, CancellationToken cancellationToken)
    {
        var query = new GetAllEnrollmentsInProgramQuery() with { ProgramId = programId, IncludeDisabled = includeDisabled, Filters = filters };
        var result = await _mediator.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }


    [HttpGet("/api/students/{userId}/enrollments")]
    [RequireUserAccess("userId")]
    [HasPermission(Permissions.GetEnrollments)]
    public async Task<IActionResult> GetAllInUser([FromRoute] int userId, [FromQuery] bool includeDisabled = false, CancellationToken cancellationToken = default)
    {
        var query = new GetAllEnrollmentsInUserQuery(includeDisabled, userId);
        var result = await _mediator.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }


    [HttpPost("/api/programs/{programId}/enrollments")]
    [RequireProgramAccess("programId")]
    [HasPermission(Permissions.CreateEnrollments)]
    public async Task<IActionResult> Create([FromRoute] int programId, [FromBody] CreateEnrollmentRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request.Adapt<CreateEnrollmentCommand>() with { ProgramId=programId }, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPut("{id}")]
    [RequireEnrollmentAccess("id")]
    [HasPermission(Permissions.UpdateEnrollments)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateEnrollmentRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request.Adapt<UpdateEnrollmentCommand>() with { Id = id }, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpDelete("{id}/toggle-status")]
    [RequireEnrollmentAccess("id")]
    [HasPermission(Permissions.ToggleStatusEnrollments)]
    public async Task<IActionResult> ToggleStatus(int id, CancellationToken cancellationToken = default)
    {
        var command = new ToggleStatusEnrollmentCommand(id);
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

}
