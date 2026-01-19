using App.Application.Commands.Authentications;
using App.Application.Responses.Faculties;
using App.Core.Entities.Identity;
using Mapster;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace App.Application.Mapping;

public class MappingConfigurations : IRegister
{
    public void Register(TypeAdapterConfig config)
    {

        config.NewConfig<RegisterCommand, ApplicationUser>()
            .Map(dest => dest.UserName, src => $"{src.FirstName}{src.LastName}");

        config.NewConfig<University, UniversityDetailResponse>()
                .Map(dest => dest.Faculties,
                    src => src.Faculties
                    .Where(f => !f.IsDeleted));


        config.NewConfig<Faculty, FacultyResponse>()
             .Map(dest => dest.NumberOfStudents,
                src => src.Departments
             .SelectMany(d => d.Programs)
             .SelectMany(p => p.Students)
             .Count());
    }
}
