using App.Application.Queries.Departments;
using App.Application.Queries.StudentsPortal;
using App.Core.Extensions;
using App.Infrastructure.Abstractions.Consts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers.StudentsPortal;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class StudentsPortalController(IMediator _mediator) : ControllerBase
{
    //TODO
    //1.0 Think about pagination


    [HttpGet("my/lectures")]
    [HasPermission(Permissions.GetStudentsPortal)]
    public async Task<IActionResult> My(CancellationToken cancellationToken = default)
    {
        var query = new GetMyLecturesQuery(User.GetUserId());
        var result = await _mediator.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
}
