using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Contracts.Responses.Enrollments;

public record EnrollmentResponse
(
    int Id,
    int ProgramId,
    int YearId,
    int TermId,
    int UserId
);
