namespace App.Core.Entities.University;
public class Department
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string HeadOfDepartment { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public int FacultyId { get; set; }
    public Faculty Faculty { get; set; } = default!;

    public ICollection<Program> Programs { get; set; } = new HashSet<Program>();
}
