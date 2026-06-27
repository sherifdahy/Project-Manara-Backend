using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Contracts.Responses.StudentPortals;

public record RegisterLectureResponse(
    int LectureScheduleId,
    int StudentId,
    decimal GPA
);
