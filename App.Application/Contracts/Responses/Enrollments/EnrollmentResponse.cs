using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Contracts.Responses.Enrollments;

public record EnrollmentResponse
(
    int Id,
    string ProgramName,
    string YearName,
    string TermName,
    int UserId,
    bool? IsActive
);
