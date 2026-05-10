using App.Application.Contracts.Responses.Roles;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Infrastructure.Mapping;

public class RoleMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        TypeAdapterConfig<ApplicationRole, RoleResponse>.NewConfig()
            .Map(dest => dest.NumberOfPermissions, src => src.RoleClaimOverrides.Count);
    }
}
