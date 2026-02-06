

using App.Core.Entities.Identity;

namespace App.Core.Entities.Personnel;

public class DepartmentUser
{
    public int UserId { get; set; }
    public int DepartmentId { get; set; }

    public Department Department { get; set; } = default!;
    public ApplicationUser User { get; set; } = default!;
}
