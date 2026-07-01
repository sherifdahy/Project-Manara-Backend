using App.Application.Contracts.Responses.Departments;
using App.Application.Contracts.Responses.StudentPortals;

namespace App.Application.Commands.StudentPortals;

public record CreateRegisterLectureCommand : IRequest<Result<RegisterLectureResponse>>
{
    public int LectureScheduleId { get; set; } 
    public int StudentId { get; set; } 

}
