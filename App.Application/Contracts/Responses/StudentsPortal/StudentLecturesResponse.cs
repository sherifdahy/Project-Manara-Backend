using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Contracts.Responses.StudentsPortal;

public record StudentLecturesResponse
(
    int? LectureScheduleId,
    SubjectDetailResponse Subject,
    decimal? GPA,
    string Status
);

public record SubjectDetailResponse
(
    int Id,
    string Name,
    int CreditHours
);
