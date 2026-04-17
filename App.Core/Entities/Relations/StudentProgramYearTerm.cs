namespace App.Core.Entities.Relations;

public class StudentProgramYearTerm
{

    public int Id { get; set; }
    public int UserId { get; set; }
    public Student User { get; set; } = default!;

    public int ProgramId { get; set; }
    public Program Program { get; set; } = default!;

    public int YearId { get; set; }
    public int TermId { get; set; }

    public YearTerm YearTerm { get; set; } = default!;
}
