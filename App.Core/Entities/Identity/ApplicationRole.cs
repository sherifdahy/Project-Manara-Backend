namespace App.Core.Entities.Identity;

public class ApplicationRole : IdentityRole<int>
{
    public bool IsDeleted { get; set; }
    public bool IsDefault { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public int ScopeId { get; set; }
    public Scope Scope { get; set; } = default!;
    public int? ParentRoleId { get; set; }
    public ApplicationRole ParentRole { get; set; } = default!;
    public ICollection<ApplicationRole> ChildRoles { get; set; } = new HashSet<ApplicationRole>();
    public ICollection<RoleClaimOverride> RoleClaimOverrides { get; set; } = new HashSet<RoleClaimOverride>();
}

