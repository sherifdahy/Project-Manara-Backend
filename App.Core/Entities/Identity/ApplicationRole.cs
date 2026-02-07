using App.Core.Entities.Universities;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Core.Entities.Identity;
public class ApplicationRole : IdentityRole<int>
{
    public bool IsDeleted { get; set; }
    public bool IsDefualt { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public ICollection<RoleClaimOverride> RoleClaimOverrides { get; set; } = new HashSet<RoleClaimOverride>();
    public int? RoleId { get; set; }
    public ApplicationRole Role { get; set; } = default!;
}
