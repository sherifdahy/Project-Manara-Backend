using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers.Instructors;

[Route("api/[controller]")]
[ApiController]
public class InstructorsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("{facultyId}")]
    public async Task<IActionResult> GetAll(int facultyId,CancellationToken cancellationToken = default)
    {
        return Ok();
    }
}
