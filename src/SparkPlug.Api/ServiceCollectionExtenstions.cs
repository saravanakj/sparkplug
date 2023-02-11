namespace SparkPlug.Api;

public static class SparkPlugApiServiceCollectionExtenstions
{
    public static IServiceCollection AddSparkPlugApi(this IServiceCollection services, WebApplicationBuilder builder, Action<SparkPlugApiOptions>? setupAction = default)
    {
        // builder.WebHost.UseUrls("http://0.0.0.0:1234/{tenant}/{version}/api");
        services.AddOptions<SparkPlugApiOptions>().BindConfiguration(SparkPlugApiOptions.ConfigPath).ValidateDataAnnotations().ValidateOnStart();
        services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<SparkPlugApiOptions>>().Value);
        // services.Configure<SparkPlugApiOptions>(configuration.GetSection(SparkPlugApiOptions.ConfigPath));
        var config = new SparkPlugApiOptions();
        builder.Configuration.Bind(SparkPlugApiOptions.ConfigPath, config);
        services.AddSwaggerApi(config);
        services.AddHealthChecks();
        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        services.AddGenericTypes();
        services.AddMvc(MvcOptions => MvcOptions.Conventions.Add(new GenericControllerRouteConvention()))
        .ConfigureApplicationPartManager(m => m.FeatureProviders.Add(new GenericTypeControllerFeatureProvider(typeof(ApiController<,,>))));
        // .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        services.AddControllers(options => options.Filters.Add(new SparkPlugExceptionFilterAttribute()));
        if (setupAction != null) services.Configure(setupAction);
        return services;
    }

    public static void UseSparkPlugApi(this WebApplication app)
    {
        var config = new SparkPlugApiOptions();
        app.Configuration.Bind(SparkPlugApiOptions.ConfigPath, config);
        if (app.Environment.IsDevelopment()) { app.UseSwaggerApi(); }
        app.UseGlobalExceptionHandling();
        app.MapGet("/", async context => await context.Response.WriteAsync("Running!..."));
        app.MapGet("/api", (IOptions<SparkPlugApiOptions> options) => options?.Value);
        app.MapHealthChecks("/health/ready", new HealthCheckOptions { Predicate = healthCheck => healthCheck.Tags.Contains("all") });
        app.MapHealthChecks("/health/live", new HealthCheckOptions { Predicate = _ => false });
        app.UseTransactionMiddleware();
        if (config.IsMultiTenant) app.UseTenantResolverMiddleware();
        // app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
    }
}
