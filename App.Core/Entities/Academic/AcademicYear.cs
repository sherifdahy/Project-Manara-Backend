namespace App.Core.Entities.Academic;
public class AcademicYear
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
      
    public ICollection<Semester> Semesters { get; set; } = new HashSet<Semester>();
}