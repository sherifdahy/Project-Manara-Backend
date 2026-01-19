using App.Infrastructure.Localization.Localizers;

namespace App.API;

public static class DependancyInjection
{
    public static void AddDepenecyInjectionRegistration(this WebApplicationBuilder builder)
    {
        builder.Services.AddApplicationRegistrations();
        builder.Services.AddInfrastructureRegistrations();
        builder.Services.AddServicesRegistration();
        builder.Services.AddServicesConfig();
        builder.Services.AddCorsConfig(builder.Configuration);
        builder.AddSeriLogConfig();
        builder.Services.AddOptionPatternsConfig(builder.Configuration);
        builder.Services.AddAuthConfig(builder.Configuration);
        builder.Services.AddScalerConfig();
        builder.Services.AddDbContextConfig(builder.Configuration);
        builder.Services.AddBackgroundJobsConfig(builder.Configuration);
        builder.Services.AddLocalizationConfig(builder.Configuration);

    }
    private static void AddSeriLogConfig(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, configuration) =>
        {
            configuration.ReadFrom.Configuration(context.Configuration);
        });
    }
    private static IServiceCollection AddScalerConfig(this IServiceCollection services)
    {
        services.AddSingleton<BearerSecuritySchemeTransformer>();

        services.AddOpenApi("v1", options =>
        {
            options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
        });

        return services;
    }
    private static IServiceCollection AddServicesConfig(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();

        services.AddProblemDetails();

        return services;
    }
    private static IServiceCollection AddAuthConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IJwtProvider, JwtProvider>();

        services.AddOptions<JwtOptions>()
            .BindConfiguration("Jwt")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        var jwtSettings = configuration.GetSection("Jwt").Get<JwtOptions>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.SaveToken = true;
            // validation 
            o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateActor = true,
                ValidateLifetime = true,

                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings!.Key)),
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                ClockSkew = TimeSpan.Zero
            };
        });

        services.AddTransient<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddTransient<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

        return services;
    }
    private static IServiceCollection AddCorsConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.WithOrigins(configuration.GetSection("AllowedOrigins").Get<string[]>()!);
            });
        });
        return services;
    }
    private static IServiceCollection AddOptionPatternsConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<JwtOptions>()
            .BindConfiguration(JwtOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.Configure<MailSettings>(configuration.GetSection(nameof(MailSettings)));

        return services;
    }

    private static void AddDbContextConfig(this IServiceCollection services, IConfiguration configuration)
    {
        var connetionString = configuration.GetConnectionString("default")
                ?? throw new InvalidOperationException("Connetion String Not Found");

        services.AddDbContext<ApplicationDbContext>(x =>
        {
            x.UseSqlServer(connetionString);
        });
    }

    private static IServiceCollection AddBackgroundJobsConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(config => config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(configuration.GetConnectionString("default")));

        services.AddHangfireServer();

        return services;
    }
    private static IServiceCollection AddLocalizationConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<JsonStringLocalizer>();

        services.AddLocalization();

        services.AddMvc();

        services.AddDistributedMemoryCache();

        services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = new[]
            {
                new CultureInfo("ar-EG"),
                new CultureInfo("en-US")
            };

            options.DefaultRequestCulture = new RequestCulture(culture: supportedCultures[0]);
            options.SupportedCultures = supportedCultures;
        });

        return services;
    }
}
