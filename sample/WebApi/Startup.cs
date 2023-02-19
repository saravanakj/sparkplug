namespace SparkPlug.Sample.Api;

public class Startup
{
    public IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSparkPlugApi(Configuration);
        // builder.Services.AddOptions<SparkPlugApiOptions>().Configure((options) => { });
        services.AddSparkPlugMongoDb(Configuration);
        services.AddScoped<ITenantResolver, TenantResolver>();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi((bearerOptions) =>
            {
                Configuration.Bind("SparkPlug:Api:AzureAd", bearerOptions);
                // bearerOptions.TokenValidationParameters.NameClaimType = "name";
            }, (identityOptions) => Configuration.Bind("SparkPlug:Api:AzureAd", identityOptions));
        // builder.Services.AddAuthentication().AddMicrosoftAccount((options) =>
        //     {
        //         options.ClientId = "";
        //         options.ClientSecret = "";
        //     });
        services.AddCors(options =>
       {
           options.AddDefaultPolicy(builder =>
           {
               builder.AllowAnyOrigin()
                      .AllowAnyHeader()
                      .AllowAnyMethod();
           });
       });
    }

    public void Configure(IApplicationBuilder app, IServiceProvider serviceProvider)
    {
        app.UseCors();
        app.UseSparkPlugApi(serviceProvider);
        app.UseSparkPlugMongoDb(serviceProvider);
    }
}
