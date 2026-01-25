using App.Core.Entities.Universities;
using App.Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Core.Entities.Identity;
public class ApplicationRole : IdentityRole<int>
{
    public bool IsDeleted { get; set; }
    public bool IsDefualt { get; set; }
    public RoleType RoleType { get; set; }

    public int? UniversityId { get; set; }

    public University University { get; set; } = default!;
}
