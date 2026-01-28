using App.Core.Entities.Identity;

namespace App.Core.Entities.Relations;

public class UserPermissionOverride
{
    public string ClaimValue { get; set; }=string.Empty;
    public bool IsAllowed { get; set; } 
    public int ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; } = default!;
}