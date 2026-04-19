using App.API.Attributes;
using App.Application.Commands.Programs;
using App.Application.Contracts.Requests.Programs;
using App.Application.Contracts.Requests.ProgramSchedules;
using App.Application.Queries.Programs;
using App.Core.Extensions;
using App.Infrastructure.Abstractions.Consts;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace App.API.Controllers.Programs
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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

        [HttpGet("{programId}/subjects")]
        [HasPermission(Permissions.GetProgramSubjects)]
        public async Task<IActionResult> GetSubjects(int programId, CancellationToken cancellationToken = default)
        {
            var query = new GetProgramSubjectsQuery() with { ProgramId = programId };
            var result = await _mediator.Send(query);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpPost("{programId}/subjects/{subjectId}")]
        [HasPermission(Permissions.AddProgramSubjects)]
        public async Task<IActionResult> AddSubject(int programId, int subjectId, CancellationToken cancellationToken = default)
        {
            var command = new AddSubjectToProgramCommand() with { ProgramId = programId, SubjectId = subjectId };
            var result = await _mediator.Send(command);
            return result.IsSuccess ? NoContent() : result.ToProblem();
        }

        [HttpDelete("{programId}/subjects/{subjectId}")]
        [HasPermission(Permissions.RemoveProgramSubjects)]
        public async Task<IActionResult> RemoveSubject(int programId, int subjectId, CancellationToken cancellationToken = default)
        {
            var command = new RemoveSubjectFromProgramCommand() with { ProgramId = programId, SubjectId = subjectId };
            var result = await _mediator.Send(command);
            return result.IsSuccess ? NoContent() : result.ToProblem();
        }

        [HttpGet("{programId}/schedule")]
        public async Task<IActionResult> GetSchedule(int programId,CancellationToken cancellationToken)
        {
            var command = new GetProgramScheduleQuery() with { ProgramId = programId };
            var result = await _mediator.Send(command, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }
        [HttpPost("{programId}/schedule")]
        public async Task<IActionResult> SaveSchdeule(int programId,CreateProgramScheduleRequest request, CancellationToken cancellationToken)
        {
            var command = new SaveProgramScheduleCommand() with { ProgramId = programId,Schedules = request.Schedules };
            var result = await _mediator.Send(command, cancellationToken);
            return result.IsSuccess ? NoContent() : result.ToProblem();
        }
    }
}
