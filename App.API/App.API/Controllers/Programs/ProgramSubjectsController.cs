using App.Application.Commands.Programs;
using App.Application.Queries.Programs;
using App.Core.Extensions;
using App.Infrastructure.Abstractions.Consts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers.Programs;

[Route("api/programs/{programId}")]
[ApiController]
public class ProgramSubjectsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("subjects")]
    [HasPermission(Permissions.GetProgramSubjects)]
    public async Task<IActionResult> GetSubjects(int programId, CancellationToken cancellationToken = default)
    {
        var query = new GetProgramSubjectsQuery() with { ProgramId = programId };
        var result = await _mediator.Send(query);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost("subjects/{subjectId}")]
    [HasPermission(Permissions.AddProgramSubjects)]
    public async Task<IActionResult> AddSubject(int programId, int subjectId, CancellationToken cancellationToken = default)
    {
        var command = new AddSubjectToProgramCommand() with { ProgramId = programId, SubjectId = subjectId };
        var result = await _mediator.Send(command);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpDelete("subjects/{subjectId}")]
    [HasPermission(Permissions.RemoveProgramSubjects)]
    public async Task<IActionResult> RemoveSubject(int programId, int subjectId, CancellationToken cancellationToken = default)
    {
        var command = new RemoveSubjectFromProgramCommand() with { ProgramId = programId, SubjectId = subjectId };
        var result = await _mediator.Send(command);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

}
