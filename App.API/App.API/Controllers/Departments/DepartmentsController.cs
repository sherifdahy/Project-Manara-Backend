using App.API.Attributes;
using App.Application.Commands.Departments;
using App.Application.Contracts.Requests.Departments;
using App.Application.Queries.Departments;
using App.Application.Queries.Faculties;
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

        [HttpGet("/api/faculties/{facultyId:int}/departments")]
        [RequireFacultyAccess("facultyId")]
        [HasPermission(Permissions.GetDepartments)]
        public async Task<IActionResult> GetAll([FromRoute] int facultyId, [FromQuery] bool includeDisabled = false, CancellationToken cancellationToken = default)
        {
            var query = new GetAllDepartmentsQuery(includeDisabled, facultyId);
            var result = await _mediator.Send(query, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpGet("{id:int}")]
        [RequireDepartmentAccess("id")]
        [HasPermission(Permissions.GetDepartments)]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken = default)
        {
            var query = new GetDepartmentQuery(id);
            var result = await _mediator.Send(query, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpGet("my")]
        [HasPermission(Permissions.GetDepartments)]
        public async Task<IActionResult> My(CancellationToken cancellationToken = default)
        {
            var query = new GetMyDepartmentQuery();
            var result = await _mediator.Send(query, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpPost("/api/faculties/{facultyId}/departments")]
        [RequireFacultyAccess("facultyId")]
        [HasPermission(Permissions.CreateDepartments)]
        public async Task<IActionResult> Create([FromRoute] int facultyId, [FromBody] DepartmentRequest request,CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request.Adapt<CreateDepartmentCommand>() with { FacultyId=facultyId}, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();  
        }


        [HttpPut("{id}")]
        [RequireDepartmentAccess("id")] 
        [HasPermission(Permissions.UpdateDepartments)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] DepartmentRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(request.Adapt<UpdateDepartmentCommand>() with { Id = id }, cancellationToken);
            return result.IsSuccess ? NoContent() : result.ToProblem();
        }

        [HttpDelete("{id}/toggle-status")]
        [RequireDepartmentAccess("id")]
        [HasPermission(Permissions.ToggleStatusDepartments)]
        public async Task<IActionResult> ToggleStatus(int id, CancellationToken cancellationToken = default)
        {
            var command = new ToggleStatusDepartmentCommand(id);
            var result = await _mediator.Send(command, cancellationToken);
            return result.IsSuccess ? NoContent() : result.ToProblem();
        }
    }
}
