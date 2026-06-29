using App.Application.Contracts.Responses.StudentPortals;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Commands.StudentPortals;

public record UpdateStudentGradeCommand : IRequest<Result>
{
    public int LectureScheduleId { get; set; }
    public int StudentId { get; set; }
    public int UserId { get; set; }
    public decimal GPA { get; set; }

}
