using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Entities.Relations;

public class ProgramSubject 
{
    public int ProgramId { get; set; }
    public Program Program { get; set; } = default!;
    public int SubjectId { get; set; }
    public Subject Subject { get; set; } = default!;
}
