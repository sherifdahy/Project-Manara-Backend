using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Infrastructure.Mapping;

public class AuthMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterCommand, ApplicationUser>()
                .Map(dest => dest.UserName, src => $"{src.FirstName}{src.LastName}");
    }
}
