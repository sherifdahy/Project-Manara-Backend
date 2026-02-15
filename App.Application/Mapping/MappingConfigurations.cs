using App.Application.Commands.Authentications;
using App.Application.Contracts.Responses.Faculties;
using App.Application.Contracts.Responses.Roles;
using App.Application.Contracts.Responses.Universities;
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
             .Map(dest => dest.NumberOfProgramUsers,
                src => src.Departments
             .SelectMany(d => d.Programs)
             .SelectMany(p => p.ProgramUsers)
             .Count());

        TypeAdapterConfig<ApplicationRole, RoleResponse>.NewConfig()
            .Map(dest => dest.NumberOfPermissions, src => src.RoleClaimOverrides.Count);
    }
}
