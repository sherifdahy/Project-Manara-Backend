using App.Core.Consts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Infrastructure.Presistance.EntitiesConfiguration;

public class ScopeConfiguration : IEntityTypeConfiguration<Scope>
{
    public void Configure(EntityTypeBuilder<Scope> builder)
    {
        builder.HasData(
            new Scope
            {
                Id = DefaultScopes.System.Id,
                Name = DefaultScopes.System.Name
            },
            new Scope
            {
                Id = DefaultScopes.University.Id,
                Name = DefaultScopes.University.Name,
                ParentScopeId = DefaultScopes.System.Id
            },
            new Scope
            {
                Id = DefaultScopes.Faculty.Id,
                Name = DefaultScopes.Faculty.Name,
                ParentScopeId = DefaultScopes.University.Id
            },
            new Scope
            {
                Id = DefaultScopes.Department.Id,
                Name = DefaultScopes.Department.Name,
                ParentScopeId = DefaultScopes.Faculty.Id
            },
            new Scope
            {
                Id = DefaultScopes.Program.Id,
                Name = DefaultScopes.Program.Name,
                ParentScopeId = DefaultScopes.Department.Id
            }
        );
    }
}
