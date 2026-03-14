using App.API.Attributes;
using App.Application.Commands.Departments;
using App.Application.Commands.Years;
using App.Application.Contracts.Requests.Departments;
using App.Application.Contracts.Requests.Years;
using App.Core.Extensions;
using App.Infrastructure.Abstractions.Consts;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers.Years;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class YearsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("/api/faculties/{facultyId}/years")]
    [RequireFacultyAccess("facultyId")]
    [HasPermission(Permissions.CreateYears)]
    public async Task<IActionResult> Create([FromRoute] int facultyId, [FromBody] YearRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request.Adapt<CreateYearCommand>() with { FacultyId = facultyId }, cancellationToken);

        //TODO GET
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPut("{id}")]
    //[RequireDepartmentAccess("id")] //TODO Filter
    [HasPermission(Permissions.UpdateYears)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] YearRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request.Adapt<UpdateYearCommand>() with { Id = id }, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
