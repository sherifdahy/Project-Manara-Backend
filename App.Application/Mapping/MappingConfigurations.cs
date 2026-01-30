using App.Application.Commands.Authentications;
using App.Application.Contracts.Responses.Faculties;
using App.Application.Contracts.Responses.Roles;
using App.Application.Contracts.Responses.Universities;
using App.Core.Entities.Identity;
using App.Core.Entities.Relations;
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

        config.NewConfig<UserPermissionOverride, AssignToUserPermissionResponse>()
            .MapWith(src =>
                new AssignToUserPermissionResponse(
                    src.ApplicationUserId,
                    src.ClaimValue,
                    src.IsAllowed
                ));

        config.NewConfig<Faculty, FacultyResponse>()
             .Map(dest => dest.NumberOfStudents,
                src => src.Departments
             .SelectMany(d => d.Programs)
             .SelectMany(p => p.Students)
             .Count());
    }
}
