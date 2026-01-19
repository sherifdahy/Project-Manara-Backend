namespace App.Application;

public static class ApplicationRegistrations
{
    public static void AddApplicationRegistrations(this IServiceCollection services)
    {
        services.AddMediatR(o => o.RegisterServicesFromAssembly(typeof(ApplicationRegistrations).Assembly));
        services
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
            .AddFluentValidationAutoValidation();

        services.AddSingleton<AuthenticationErrors>();
        services.AddSingleton<FacultyErrors>();
        services.AddSingleton<UniversityErrors>();
        services.AddSingleton<RoleErrors>();
        services.AddSingleton<PermissionErrors>();

    }
}
