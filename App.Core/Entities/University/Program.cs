namespace App.Core.Entities.University;

public class Program

{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int CreditHours { get; set; }

    public int DepartmentId { get; set; }
    public Department Department { get; set; } = default!;

    public ICollection<Subject> Subjects { get; set; } = new HashSet<Subject>();    
    public ICollection<Student> Students { get; set; } = new HashSet<Student>();
}
