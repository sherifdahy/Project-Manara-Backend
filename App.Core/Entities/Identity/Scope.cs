namespace App.Core.Entities.Identity;

public class Scope
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }
    public int? ParentScopeId { get; set; }
    public Scope ParentScope { get; set; } = default!;
    public ICollection<Scope> ChildScopes { get; set; } = new HashSet<Scope>();
    public ICollection<ApplicationRole> Roles { get; set; } = new HashSet<ApplicationRole>();
}