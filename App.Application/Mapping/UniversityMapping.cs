using App.Application.Contracts.Responses.Universities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Infrastructure.Mapping;

public class UniversityMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<University, UniversityDetailResponse>()
        .Map(dest => dest.Faculties,
            src => src.Faculties
            .Where(f => !f.IsDeleted));
    }
}
