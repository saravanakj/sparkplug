namespace SparkPlug.Sample.Api;

public static class Program
{
    public static void Main(string[] args)
    {
        WebApplication.CreateBuilder(args)
                .AddService().Build()
                .UseConfigure().Run();
    }

    public static WebApplicationBuilder AddService(this WebApplicationBuilder builder)
    {
        builder.Services.AddSparkPlugApi();
        // builder.Services.AddOptions<SparkPlugApiOptions>().Configure((options) => { });
        builder.Services.AddSparkPlugMongoDb();
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi((bearerOptions) =>
            {
                builder.Configuration.Bind("SparkPlug:Api:AzureAd", bearerOptions);
                // bearerOptions.TokenValidationParameters.NameClaimType = "name";
            }, (identityOptions) => builder.Configuration.Bind("SparkPlug:Api:AzureAd", identityOptions));
        // builder.Services.AddAuthentication().AddMicrosoftAccount((options) =>
        //     {
        //         options.ClientId = "";
        //         options.ClientSecret = "";
        //     });
        builder.Services.AddDependencyInjection();
        return builder;
    }

    public static WebApplication UseConfigure(this WebApplication app)
    {
        app.UseSparkPlugApi();
        app.UseSparkPlugMongoDb();
        return app;
    }

    public static void AddDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<PersonRepository>();
        services.AddScoped<DepartmentRepository>();
    }
}
