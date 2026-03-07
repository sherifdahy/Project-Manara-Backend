using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Entities.Relations;

public class ProgramUserProgramYearTerm
{
    public int UserId { get; set; }
    public ProgramUser User { get; set; } = default!;

    public int ProgramId { get; set; }
    public Program Program { get; set; } = default!;

    public int YearTermId { get; set; }
    public YearTerm YearTerm { get; set; } = default!;
}
