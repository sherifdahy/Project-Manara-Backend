using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Contracts.Requests.StudentPortals;

public record RegisterLectureRequest
{
    public int LectureScheduleId { get; set; } 
}
