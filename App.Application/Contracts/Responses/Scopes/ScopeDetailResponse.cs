using App.Application.Contracts.Responses.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Contracts.Responses.Scopes;

public record ScopeDetailResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }
    public int? ParentScopeId { get; set; }
    public ScopeResponse? ParentScope { get; set; }
    public List<ScopeResponse> ChildScopes { get; set; } = [];
    public List<RoleResponse> Roles { get; set; } = [];
}
