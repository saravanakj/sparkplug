namespace SparkPlug.Api;

public static class SwaggerServiceCollectionExtensions
{
    private static readonly string?[] _apiVersions;

    private static Assembly[]? _assemblies;

    public static Assembly[] CachedAssemblies { get => _assemblies ??= AppDomain.CurrentDomain.GetAssemblies(); }

    static SwaggerServiceCollectionExtensions()
    {
        _apiVersions = CachedAssemblies
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.IsDefined(typeof(ApiExplorerSettingsAttribute), false))
            .SelectMany(type => type.GetCustomAttributes<ApiExplorerSettingsAttribute>())
            .Select(a => a.GroupName)
            .Distinct()
            .ToArray();
    }

    public static IServiceCollection AddSwaggerApi(this IServiceCollection services, SparkPlugApiOptions options)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(swaggerOptions =>
            {
                Array.ForEach(_apiVersions, version => swaggerOptions.SwaggerDoc(version, new OpenApiInfo { Version = version, Title = "Api" }));
                swaggerOptions.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                swaggerOptions.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                swaggerOptions.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference{ Type=ReferenceType.SecurityScheme, Id="Bearer"}
                        },
                        Array.Empty<string>()
                    }
                });
            });
        return services;
    }

    public static void UseSwaggerApi(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(uiOptions => Array.ForEach(_apiVersions, version => uiOptions.SwaggerEndpoint($"/swagger/{version}/swagger.json", version)));
    }
}