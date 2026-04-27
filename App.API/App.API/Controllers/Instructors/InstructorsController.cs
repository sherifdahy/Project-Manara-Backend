using App.Application.Abstractions;
using App.Application.Queries.Doctors;
using App.Core.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers.Instructors;

[Route("api/[controller]")]
[ApiController]
public class InstructorsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("{facultyId}")]
    public async Task<IActionResult> GetAll([FromRoute] int facultyId, [FromQuery] RequestFilters filters, CancellationToken cancellationToken = default)
    {
        var query = new GetAllDoctorsInsideFacultyQuery(facultyId, filters);
        var result = await _mediator.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result) : result.ToProblem();
    }
}
