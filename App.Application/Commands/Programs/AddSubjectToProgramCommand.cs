using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Commands.Programs;

public record AddSubjectToProgramCommand : IRequest<Result>
{
    public int ProgramId { get; set; }
    public int SubjectId { get; set; }
}
