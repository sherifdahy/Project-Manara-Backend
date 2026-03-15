using App.API.Attributes;
using App.Application.Commands.Departments;
using App.Application.Commands.Years;
using App.Application.Contracts.Requests.Years;
using App.Application.Queries.Years;
using App.Core.Extensions;
using App.Infrastructure.Abstractions.Consts;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers.Years;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class YearsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;


    [HttpGet("/api/faculties/{facultyId:int}/years")]
    [RequireFacultyAccess("facultyId")]
    [HasPermission(Permissions.GetYears)]
    public async Task<IActionResult> GetAll([FromRoute] int facultyId, [FromQuery] bool includeDisabled = false, CancellationToken cancellationToken = default)
    {
        var query = new GetAllYearsQuery(includeDisabled, facultyId);
        var result = await _mediator.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("{id:int}")]
    [RequireYearAccess("id")]
    [HasPermission(Permissions.GetYears)]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken = default)
    {
        var query = new GetYearQuery(id);
        var result = await _mediator.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost("/api/faculties/{facultyId}/years")]
    [RequireFacultyAccess("facultyId")]
    [HasPermission(Permissions.CreateYears)]
    public async Task<IActionResult> Create([FromRoute] int facultyId, [FromBody] YearRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request.Adapt<CreateYearCommand>() with { FacultyId = facultyId }, cancellationToken);

        return result.IsSuccess ? CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value) : result.ToProblem();
    }

    [HttpPut("{id}")]
    [RequireYearAccess("id")] 
    [HasPermission(Permissions.UpdateYears)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] YearRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request.Adapt<UpdateYearCommand>() with { Id = id }, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpDelete("{id}/toggle-status")]
    [RequireYearAccess("id")]
    [HasPermission(Permissions.ToggleStatusYears)]
    public async Task<IActionResult> ToggleStatus(int id, CancellationToken cancellationToken = default)
    {
        var command = new ToggleStatusYearCommand(id);
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
