using App.API.Attributes;
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

    [HttpGet("students/{studentId:int}/lectures")]
    [RequireStudentAccess("studentId")]
    [HasPermission(Permissions.GetStudentsPortal)]
    public async Task<IActionResult> GetStudentLectures(int studentId, CancellationToken cancellationToken = default)
    {
        var query = new GetStudentLecturesQuery(studentId);
        var result = await _mediator.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }


    [HttpGet("students/{studentId:int}/available-lectures")]
    [RequireStudentAccess("studentId")]
    [HasPermission(Permissions.GetStudentsPortal)]
    public async Task<IActionResult> GetStudentAvailableLectures(int studentId, CancellationToken cancellationToken = default)
    {
        var query = new GetStudentAvailableLecturesQuery(studentId);
        var result = await _mediator.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }



    [HttpPost("students/{studentId:int}/available-lectures")]
    [RequireStudentAccess("studentId")]
    [HasPermission(Permissions.CreateStudentsPortal)]
    public async Task<IActionResult> RegisterStudentLecture(int studentId, [FromBody] RegisterLectureRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request.Adapt<CreateRegisterLectureCommand>() with { StudentId = studentId }, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPut("students/{studentId:int}/gpa")]
    [RequireStudentAccess("studentId")]
    [HasPermission(Permissions.UpdateStudentsGrade)]

    public async Task<IActionResult> UpdateGrade(int studentId, [FromBody] UpdateStudentGradeRequest request,CancellationToken cancellationToken = default)
    {
        var command = request.Adapt<UpdateStudentGradeCommand>() with {  StudentId=studentId,UserId=User.GetUserId()};

        var result = await _mediator.Send(command, cancellationToken);

        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

}
