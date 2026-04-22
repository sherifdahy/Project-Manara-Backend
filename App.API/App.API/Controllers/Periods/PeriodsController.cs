using App.Application.Commands.Periods;
using App.Application.Contracts.Requests.Periods;
using App.Application.Queries.Periods;
using App.Core.Extensions;
using App.Infrastructure.Abstractions.Consts;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers.Periods;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PeriodsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("/api/faculties/{facultyId:int}/periods")]
    [RequireFacultyAccess("facultyId")]
    [HasPermission(Permissions.GetPeriods)]
    public async Task<IActionResult> GetAll
        ([FromRoute] int facultyId, [FromQuery] bool includeDisabled = false, CancellationToken cancellationToken = default)
    {
        var query = new GetAllPeriodsQuery(includeDisabled, facultyId);
        var result = await _mediator.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("{id}")]
    [RequireFacultyAccess("facultyId")]
    [HasPermission(Permissions.GetPeriods)]
    public async Task<IActionResult> Get([FromRoute] int id,CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetPeriodQuery() with { Id = id },cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost("/api/faculties/{facultyId}/periods")]
    [RequireFacultyAccess("facultyId")]
    [HasPermission(Permissions.CreatePeriods)]
    public async Task<IActionResult> Create([FromRoute] int facultyId, [FromBody] PeriodRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request.Adapt<CreatePeriodCommand>() with { FacultyId = facultyId }, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPut("/api/faculties/{facultyId}/periods/{oldStartTime}/{oldEndTime}")]
    [RequireFacultyAccess("facultyId")]
    [HasPermission(Permissions.UpdatePeriods)]
    public async Task<IActionResult> Update(
        [FromRoute] int facultyId,
        [FromRoute] string oldStartTime,      
        [FromRoute] string oldEndTime,        
        [FromBody] PeriodRequest request,
        CancellationToken cancellationToken = default)
    {
        var parsedOldStartTime = TimeOnly.Parse(oldStartTime);
        var parsedOldEndTime = TimeOnly.Parse(oldEndTime);

        var result = await _mediator.Send(
            request.Adapt<UpdatePeriodCommand>() with
            {
                FacultyId = facultyId,
                OldStartTime = parsedOldStartTime,
                OldEndTime = parsedOldEndTime
            }, cancellationToken);

        return result.IsSuccess ? NoContent() : result.ToProblem();
    }


    [HttpDelete("/api/faculties/{facultyId}/periods/{startTime}/{endTime}")]
    [RequireFacultyAccess("facultyId")]
    [HasPermission(Permissions.ToggleStatusPeriods)]
    public async Task<IActionResult> ToggleStatus(
        [FromRoute] int facultyId,
        [FromRoute] string startTime,
        [FromRoute] string endTime,
    CancellationToken cancellationToken = default)
    {
        var parsedStartTime = TimeOnly.Parse(startTime);
        var parsedEndTime = TimeOnly.Parse(endTime);

        var result = await _mediator.Send(new ToggleStatusPeriodCommand
        {
            FacultyId = facultyId,
            StartTime = parsedStartTime,
            EndTime = parsedEndTime
        }, cancellationToken);

        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
