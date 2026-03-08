using App.API.Attributes;
using App.Application.Commands.Departments;
using App.Application.Commands.Subjects;
using App.Application.Contracts.Requests.Departments;
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

    [HttpPut("{id}")]
    [RequireSubjectAccess("id")]
    [HasPermission(Permissions.UpdateSubjects)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] SubjectRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request.Adapt<UpdateSubjectCommand>() with { Id = id }, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpDelete("{id}/toggle-status")]
    [RequireSubjectAccess("id")]
    [HasPermission(Permissions.ToggleStatusSubjects)]
    public async Task<IActionResult> ToggleStatus(int id, CancellationToken cancellationToken = default)
    {
        var command = new ToggleStatusSubjectCommand(id);
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

}
