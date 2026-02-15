using App.Application.Commands.Faculties;
using App.Application.Commands.Universities;
using App.Application.Contracts.Requests.Faculties;
using App.Application.Queries.Faculties;
using App.Core.Extensions;
using App.Infrastructure.Abstractions.Consts;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers.Faculties;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class FacultiesController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("/api/universities/{universityId:int}/[controller]")]
    [RequireUniversityAccess("universityId")]
    [HasPermission(Permissions.GetFaculties)]
    public async Task<IActionResult> GetAll([FromRoute] int universityId, [FromQuery] bool includeDisabled = false, CancellationToken cancellationToken = default)
    {
        var query = new GetAllFacultiesQuery(includeDisabled, universityId);
        var result = await _mediator.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("{id:int}")]
    [RequireFacultyAccess("id")]
    [HasPermission(Permissions.GetFaculties)]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken = default)
    {
        var query = new GetFacultyQuery(id);
        var result = await _mediator.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("my")]
    [HasPermission(Permissions.GetFaculties)]
    public async Task<IActionResult> My(CancellationToken cancellationToken = default)
    {
        var query = new GetMyFacultyQuery();
        var result = await _mediator.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }


    [HttpPost("/api/universities/{universityId:int}/[controller]")] 
    [RequireUniversityAccess("universityId")]
    [HasPermission(Permissions.CreateFaculties)]
    public async Task<IActionResult> Create([FromRoute] int universityId,[FromBody]FacultyRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request.Adapt<CreateFacultyCommand>() with { UniversityId=universityId}, cancellationToken);
        return result.IsSuccess ? CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value) : result.ToProblem();
    }

    [HttpPut("{id}")]
    [RequireFacultyAccess("id")]
    [HasPermission(Permissions.UpdateFaculties)]
    public async Task<IActionResult> Update([FromRoute] int id,[FromBody]FacultyRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request.Adapt<UpdateFacultyCommand>() with { Id = id }, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpDelete("{id}/toggle-status")]
    [RequireFacultyAccess("id")]
    [HasPermission(Permissions.ToggleStatusFaculties)]
    public async Task<IActionResult> ToggleStatus(int id, CancellationToken cancellationToken = default)
    {
        var command = new ToggleStatusFacultyCommand(id);
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
