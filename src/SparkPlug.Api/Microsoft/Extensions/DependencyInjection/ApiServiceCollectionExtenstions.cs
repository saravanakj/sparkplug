namespace Microsoft.Extensions.DependencyInjection;

public static class ApiServiceCollectionExtenstions
{
    public static IServiceCollection AddWebApi(this IServiceCollection services, IConfiguration configuration, Action<WebApiOptions>? setupAction = default)
    {
        services.AddHttpContextAccessor();
        services.AddOptions();
        services.Configure<WebApiOptions>(configuration.GetSection(WebApiOptions.ConfigPath));
        services.AddScoped<IRequestContext, RequestContext>();
        services.AddScoped(typeof(Repository<,>));
        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        services.AddScoped(typeof(ITenantOptions<>), typeof(TenantOptionsManager<>));
        services.AddScoped(sp => sp.GetRequiredService<IHttpContextAccessor>().HttpContext?.Items["Tenant"] as ITenant ?? Tenant.Default);
        services.AddMvc(MvcOptions => MvcOptions.Conventions.Add(new GenericControllerRouteConvention()))
                .ConfigureApplicationPartManager(m => m.FeatureProviders.Add(new GenericTypeControllerFeatureProvider(typeof(ApiController<,>))));
        // .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        services.AddControllers(options => options.Filters.Add<ApiExceptionFilterAttribute>())
                .AddNewtonsoftJson();
        if (setupAction != null) services.Configure(setupAction);
        services.AddSwagger();
        return services;
    }

    public static void UseWebApi(this IApplicationBuilder app, IServiceProvider serviceProvider)
    {
        var config = serviceProvider.GetRequiredService<IOptions<WebApiOptions>>();
        var env = serviceProvider.GetRequiredService<IWebHostEnvironment>();
        app.UsePathBase(config.Value.PathBase);
        if (env.IsDevelopment()) { app.UseSwagger(); }
        app.UseGlobalExceptionHandling();
        app.UseTransactionMiddleware();
        app.UseTenantResolverMiddleware();
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
