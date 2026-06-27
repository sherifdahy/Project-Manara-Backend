using App.Application.Commands.Departments;
using App.Application.Commands.StudentPortals;
using App.Application.Contracts.Requests.StudentPortals;
using App.Application.Queries.Departments;
using App.Application.Queries.StudentsPortal;
using App.Core.Extensions;
using App.Infrastructure.Abstractions.Consts;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers.StudentsPortal;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class StudentsPortalController(IMediator _mediator) : ControllerBase
{

    [HttpGet("my/lectures")]
    [HasPermission(Permissions.GetStudentsPortal)]
    public async Task<IActionResult> My(CancellationToken cancellationToken = default)
    {
        var query = new GetMyLecturesQuery(User.GetUserId());
        var result = await _mediator.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }


    [HttpPost("my/lectures")]
    [HasPermission(Permissions.CreateStudentsPortal)]
    public async Task<IActionResult> My([FromBody] RegisterLectureRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request.Adapt<CreateRegisterLectureCommand>() with {UserId=User.GetUserId() }, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

}
