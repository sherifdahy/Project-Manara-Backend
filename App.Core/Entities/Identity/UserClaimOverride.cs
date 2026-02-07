namespace App.Core.Entities.Identity;

public class UserClaimOverride
{
    public string ClaimValue { get; set; }=string.Empty;
    public int ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; } = default!;
}