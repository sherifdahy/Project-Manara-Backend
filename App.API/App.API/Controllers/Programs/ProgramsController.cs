using App.API.Attributes;
using App.Application.Commands.Departments;
using App.Application.Commands.Programs;
using App.Application.Contracts.Requests.Departments;
using App.Application.Contracts.Requests.Programs;
using App.Application.Queries.Departments;
using App.Application.Queries.Programs;
using App.Core.Extensions;
using App.Infrastructure.Abstractions.Consts;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers.Programs
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;


        [HttpGet("/api/departments/{departmentId:int}/programs")]
        [RequireDepartmentAccess("departmentId")]
        [HasPermission(Permissions.GetPrograms)]
        public async Task<IActionResult> GetAll([FromRoute] int departmentId, [FromQuery] bool includeDisabled = false, CancellationToken cancellationToken = default)
        {
            var query = new GetAllProgramsQuery(includeDisabled, departmentId);
            var result = await _mediator.Send(query, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpGet("{id:int}")]
        [RequireProgramAccess("id")]
        [HasPermission(Permissions.GetPrograms)]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken = default)
        {
            var query = new GetProgramQuery(id);
            var result = await _mediator.Send(query, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpGet("my")]
        [HasPermission(Permissions.GetPrograms)]
        public async Task<IActionResult> My(CancellationToken cancellationToken = default)
        {
            var query = new GetMyProgramQuery();
            var result = await _mediator.Send(query, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpPost("/api/departments/{departmentId}/programs")]
        [RequireDepartmentAccess("departmentId")]
        [HasPermission(Permissions.CreatePrograms)]
        public async Task<IActionResult> Create([FromRoute] int departmentId, [FromBody] ProgramRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request.Adapt<CreateProgramCommand>() with { DepartmentId = departmentId }, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpPut("{id}")]
        [RequireProgramAccess("id")]
        [HasPermission(Permissions.UpdatePrograms)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ProgramRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(request.Adapt<UpdateProgramCommand>() with { Id = id }, cancellationToken);
            return result.IsSuccess ? NoContent() : result.ToProblem();
        }

        [HttpDelete("{id}/toggle-status")]
        [RequireProgramAccess("id")]
        [HasPermission(Permissions.ToggleStatusPrograms)]
        public async Task<IActionResult> ToggleStatus(int id, CancellationToken cancellationToken = default)
        {
            var command = new ToggleStatusProgramCommand(id);
            var result = await _mediator.Send(command, cancellationToken);
            return result.IsSuccess ? NoContent() : result.ToProblem();
        }
    }
}
