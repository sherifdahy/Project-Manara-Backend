using App.Application.Queries.Departments;
using App.Infrastructure.Abstractions.Consts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers.StudentsPortal;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class StudentsPortalController : ControllerBase
{

    //Pagination
    //


    [HttpGet("my/subjects")]
    [HasPermission(Permissions.GetStudentsPortal)]
    public async Task<IActionResult> My(CancellationToken cancellationToken = default)
    {
        //var query = new GetMyDepartmentQuery();
        //var result = await _mediator.Send(query, cancellationToken);
        //return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
}
