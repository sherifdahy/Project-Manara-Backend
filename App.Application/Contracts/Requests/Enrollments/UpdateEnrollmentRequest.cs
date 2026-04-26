using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Contracts.Requests.Enrollments;

public record UpdateEnrollmentRequest
{
    public int YearId { get; set; }
    public int TermId { get; set; }
}
