using App.Infrastructure.Email;
using App.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity.UI.Services;
using SA.Accountring.Core.Entities.Interfaces;

namespace App.Infrastructure;
public static class InfrastructureRegistrations
{
   
    public static void AddInfrastructureRegistrations(this IServiceCollection services)
    {
        services.AddIdentityConfig();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IEmailSender, EmailSender>();
    }

    private static void AddIdentityConfig(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequiredLength = 8;
            options.User.RequireUniqueEmail = true;
        });
    }


}
