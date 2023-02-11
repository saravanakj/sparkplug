namespace SparkPlug.Api;

public static class SparkPlugApiServiceCollectionExtenstions
{
    public static IServiceCollection AddSparkPlugApi(this IServiceCollection services, IConfiguration configuration, Action<SparkPlugApiOptions>? setupAction = default)
    {
        services.AddOptions();
        var options = configuration.GetSection(SparkPlugApiOptions.ConfigPath);
        services.Configure<SparkPlugApiOptions>(options);
        var config = options.Get<SparkPlugApiOptions>() ?? throw new Exception("Api Options not configured");
        services.AddSwagger(config);
        services.AddTransient(typeof(IOptions<>), typeof(OptionsManager<>));
        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        services.AddGenericTypes();
        services.AddMvc(MvcOptions => MvcOptions.Conventions.Add(new GenericControllerRouteConvention()))
        .ConfigureApplicationPartManager(m => m.FeatureProviders.Add(new GenericTypeControllerFeatureProvider(typeof(ApiController<,,>))));
        // .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        services.AddControllers(options => options.Filters.Add(new SparkPlugExceptionFilterAttribute()));
        if (setupAction != null) services.Configure(setupAction);
        return services;
    }

    public static void UseSparkPlugApi(this IApplicationBuilder app, IServiceProvider serviceProvider)
    {
        var config = serviceProvider.GetRequiredService<IOptions<SparkPlugApiOptions>>();
        var env = serviceProvider.GetRequiredService<IWebHostEnvironment>();
        app.UsePathBase(config.Value.PathBase);
        if (env.IsDevelopment()) { app.UseSwagger(); }
        app.UseGlobalExceptionHandling();
        app.UseTransactionMiddleware();
        if (config.Value.IsMultiTenant) app.UseTenantResolverMiddleware();
        // app.UseHttpsRedirection();
        app.UseSwaggerApi();
        app.UseHealthChecks();
        app.UseRouting();
        app.UseEndpoints(endpoints => endpoints.MapGet("/", async context => await context.Response.WriteAsync("Running!...")));
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}
