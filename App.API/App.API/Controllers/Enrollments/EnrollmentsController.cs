using App.API.Attributes;
using App.Application.Commands.Enrollments;
using App.Application.Contracts.Requests.Enrollments;
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

    [HttpPost("/api/students/{userId}/enrollments")]
    [RequireUserAccess("userId")]
    [HasPermission(Permissions.CreateEnrollments)]
    public async Task<IActionResult> Create([FromRoute] int userId, [FromBody] EnrollmentRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request.Adapt<CreateEnrollmentCommand>() with { UserId=userId }, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("test/test/{id}")]
    [RequireEnrollmentAccess("id")]
    public async Task<IActionResult> test(int id)
    {
        return Ok();
    }

}
