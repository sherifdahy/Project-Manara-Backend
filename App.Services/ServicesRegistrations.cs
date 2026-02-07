using App.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Services;

public static class ServicesRegistrations
{
    public static void AddServicesRegistration(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IUniversityService, UniversityService>();
        services.AddScoped<IFacultyService, FacultyService>();
    }
}
