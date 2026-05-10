using App.API.Attributes;
using App.Application.Commands.Programs;
using App.Application.Contracts.Requests.LectureSchedules;
using App.Application.Contracts.Requests.SectionSchedules;
using App.Application.Queries.Programs;
using App.Core.Extensions;
using App.Infrastructure.Abstractions.Consts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers.Programs;

[Route("api/programs/{programId}")]
[ApiController]
public class ProgramSchedulesController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("lectures-schedule")]
    [RequireProgramAccess("programId")]
    [HasPermission(Permissions.GetProgramLecturesSchedule)]
    public async Task<IActionResult> GetLectureSchedule(int programId, CancellationToken cancellationToken)
    {
        var command = new GetLectureSchedulesQuery(programId);
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("sections-schedule")]
    [RequireProgramAccess("programId")]
    [HasPermission(Permissions.GetProgramSectionsSchedule)]
    public async Task<IActionResult> GetSectionSchedule(int programId, CancellationToken cancellationToken)
    {
        var command = new GetSectionSchedulesQuery(programId);
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost("lectures-schedule")]
    [RequireProgramAccess("programId")]
    [HasPermission(Permissions.SaveProgramLecturesSchedule)]
    public async Task<IActionResult> SaveLecturesSchdeule(int programId, SaveLectureSchedulesRequest request, CancellationToken cancellationToken)
    {
        var command = new SaveLectureSchedulesCommand(programId, request.Schedules);
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpPost("sections-schedule")]
    [RequireProgramAccess("programId")]
    [HasPermission(Permissions.SaveProgramSectionsSchedule)]
    public async Task<IActionResult> SaveSectionsSchdeule(int programId, SaveSectionSchedulesRequest request, CancellationToken cancellationToken)
    {
        var command = new SaveSectionSchedulesCommand(programId, request.Schedules);
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
