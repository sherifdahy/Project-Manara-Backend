using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Commands.Enrollments;

public record UpdateEnrollmentCommand : IRequest<Result>
{
    public int Id { get; init; }
    public int ProgramId { get; set; }
    public int YearId { get; set; }
    public int TermId { get; set; }
}
