using App.Core.Entities.Identity;

namespace App.Core.Entities.Relations;

public class UserPermissionOverride
{
    public bool IsAllowed { get; set; } = false;
    public int ApplicationUserId { get; set; }
    public int RoleClaimId { get; set; }  
    public ApplicationUser ApplicationUser { get; set; } = default!;
    public IdentityRoleClaim<int> RoleClaim { get; set; } = default!;
}