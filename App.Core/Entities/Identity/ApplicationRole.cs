using App.Core.Entities.Universities;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Core.Entities.Identity;
public class ApplicationRole : IdentityRole<int>
{
    public bool IsDeleted { get; set; }
    public bool IsDefualt { get; set; }
    public ICollection<RoleClaimOverride> RoleClaimOverrides { get; set; } = new HashSet<RoleClaimOverride>();

}
