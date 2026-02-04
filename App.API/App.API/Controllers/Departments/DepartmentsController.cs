using App.Application.Commands.Departments;
using App.Application.Contracts.Requests.Departments;
using App.Core.Extensions;
using App.Infrastructure.Abstractions.Consts;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers.Departments
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DepartmentsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("faculties/{facultyId}/departments")]
        [RequireFacultyAccess("facultyId")]
        [HasPermission(Permissions.CreateDepartments)]
        public async Task<IActionResult> Create([FromRoute] int facultyId, [FromBody] DepartmentRequest request,CancellationToken cancellationToken)
        {

            var result = await _mediator.Send(request.Adapt<CreateDepartmentCommand>() with { FacultyId=facultyId}, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();  
            //var result = await _mediator.Send(request.Adapt<CreateFacultyCommand>(), cancellationToken);
            //return result.IsSuccess ? CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value) : result.ToProblem();
        }
    }
}
