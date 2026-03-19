using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Entities.Academic;

public class Day
{
    public int Id { get; set; }
    public string Value { get; set; }=string.Empty;
    public ICollection<Period> Periods { get; set; } = new HashSet<Period>();
}
