using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Entities.Academic;

public class Day
{
    public int Id { get; set; }
    public DateOnly Value { get; set; }
    public ICollection<Period> Periods { get; set; } = new HashSet<Period>();
    public int FacultyId { get; set; }
    public Faculty Faculty { get; set; } = default!;
}
