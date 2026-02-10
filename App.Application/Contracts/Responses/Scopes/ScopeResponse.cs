namespace App.Application.Contracts.Responses.Scopes;

public record ScopeResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }
    public int? ParentScopeId { get; set; }
    public string? ParentScopeName { get; set; }
    public int ChildScopesCount { get; set; } 
    public int RolesCount { get; set; } 
}
