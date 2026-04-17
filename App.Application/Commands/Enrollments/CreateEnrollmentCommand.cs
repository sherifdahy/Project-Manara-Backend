using App.Application.Contracts.Responses.Departments;
using App.Application.Contracts.Responses.Enrollments;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Commands.Enrollments;

public record CreateEnrollmentCommand : IRequest<Result<EnrollmentResponse>>
{
    public int UserId { get; set; }
    public int ProgramId { get; set; }
    public int YearId { get; set; }
    public int TermId { get; set; }

}
