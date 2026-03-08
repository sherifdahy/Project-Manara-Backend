using App.Application.Commands.Subjects;
using App.Application.Contracts.Requests.Subjects;
using App.Core.Extensions;
using App.Infrastructure.Abstractions.Consts;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers.Subjects;

[Route("api/[controller]")]
[Authorize]
[ApiController]

public class SubjectsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("/api/faculties/{facultyId}/subjects")]
    [RequireFacultyAccess("facultyId")]
    [HasPermission(Permissions.CreateSubjects)]
    public async Task<IActionResult> Create([FromRoute] int facultyId, [FromBody] SubjectRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request.Adapt<CreateSubjectCommand>() with { FacultyId=facultyId}, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        //TODO
        //return result.IsSuccess ? CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value) : result.ToProblem();
    }

}
