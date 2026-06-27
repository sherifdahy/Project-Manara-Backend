using App.Application.Mapping;
using App.Core.Entities.Personnel;
using App.Infrastructure;

namespace App.Application;

public static class ApplicationRegistrations
{
    public static void AddApplicationRegistrations(this IServiceCollection services)
    {

        services.AddMapsterConfig();
        services.AddMediatR(o => o.RegisterServicesFromAssembly(typeof(ApplicationRegistrations).Assembly));
        services
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
            .AddFluentValidationAutoValidation();



        services.AddSingleton<AuthenticationErrors>();
        services.AddSingleton<FacultyErrors>();
        services.AddSingleton<UniversityErrors>();
        services.AddSingleton<RoleErrors>();
        services.AddSingleton<PermissionErrors>();
        services.AddSingleton<UserErrors>();
        services.AddSingleton<ScopeErrors>();
        services.AddSingleton<DepartmentErrors>();
        services.AddSingleton<ProgramErrors>();
        services.AddSingleton<SubjectErrors>();
        services.AddSingleton<YearErrors>();
        services.AddSingleton<ProgramUserErrors>();
        services.AddSingleton<PeriodErrors>();
        services.AddSingleton<DayErrors>();
        services.AddSingleton<EnrollmentErrors>();
        services.AddSingleton<RegistrationErrors>();
    }

    public static IServiceCollection AddMapsterConfig(this IServiceCollection services)
    {
        TypeAdapterConfig.GlobalSettings.Scan(typeof(ApplicationRegistrations).Assembly);

        return services;
    }
}
