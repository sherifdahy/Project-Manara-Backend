using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Contracts.Requests.Enrollments;

public record EnrollmentRequest
{
    public List<int> StudentIds { get; set; } = [];
    public int YearId { get; set; }
    public int TermId { get; set; }
}
