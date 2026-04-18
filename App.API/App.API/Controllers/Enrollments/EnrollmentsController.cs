using App.API.Attributes;
using App.Application.Commands.Departments;
using App.Application.Commands.Enrollments;
using App.Application.Contracts.Requests.Departments;
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

    [HttpPut("{id}")]
    [RequireEnrollmentAccess("id")]
    [HasPermission(Permissions.UpdateEnrollments)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] EnrollmentRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request.Adapt<UpdateEnrollmentCommand>() with { Id = id }, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

}
