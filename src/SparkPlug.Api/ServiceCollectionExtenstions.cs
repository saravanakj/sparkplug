namespace SparkPlug.Api;

public static class SparkPlugApiServiceCollectionExtenstions
{
    public static IServiceCollection AddSparkPlugApi(this IServiceCollection services, Action<SparkPlugApiOptions>? setupAction = default)
    {
        services.AddOptions<SparkPlugApiOptions>()
                .BindConfiguration(SparkPlugApiOptions.ConfigPath)
                .ValidateDataAnnotations()
                .ValidateOnStart();
        services.AddSwaggerApi();
        services.AddHealthChecks();
        services.AddMvc(MvcOptions => MvcOptions.Conventions.Add(new GenericControllerRouteConvention()))
                .ConfigureApplicationPartManager(m => m.FeatureProviders.Add(new GenericTypeControllerFeatureProvider(typeof(ApiController<,,>))));
        // .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        services.AddControllers(options => options.Filters.Add(new SparkPlugExceptionFilterAttribute()));
        services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<SparkPlugApiOptions>>().Value);
        if (setupAction != null) services.Configure(setupAction);
        return services;
    }

    public static void UseSparkPlugApi(this WebApplication app)
    {
        if (app.Environment.IsDevelopment()) { app.UseSwaggerApi(); }
        app.MapGet("/", async context => await context.Response.WriteAsync("Running!..."));
        app.MapGet("/api", (IOptions<SparkPlugApiOptions> options) => options?.Value);
        app.MapHealthChecks("/health/ready", new HealthCheckOptions { Predicate = healthCheck => healthCheck.Tags.Contains("all") });
        app.MapHealthChecks("/health/live", new HealthCheckOptions { Predicate = _ => false });
        app.UseTransactionMiddleware();
        // app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.UseGlobalExceptionHandling();
    }
}
