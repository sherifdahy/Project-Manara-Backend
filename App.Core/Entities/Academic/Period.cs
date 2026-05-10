using App.Core.Entities.Relations;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Entities.Academic;

public class Period
{
    public int Id { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }

    public int FacultyId { get; set; }
    public Faculty Faculty { get; set; } = default!;

    public bool IsDeleted { get; set; }

    public ICollection<LectureSchedule> LectureSchedules { get; set; } = new HashSet<LectureSchedule>();
    public ICollection<SectionSchedule> SectionSchedules { get; set; } = new HashSet<SectionSchedule>();
}
