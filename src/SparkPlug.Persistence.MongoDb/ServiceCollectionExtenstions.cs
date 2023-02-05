namespace SparkPlug.Persistence.MongoDb;

public static class SparkPlugMongoDbServiceCollectionExtenstions
{
    public static void AddSparkPlugMongoDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<SparkPlugMongoDbOptions>().BindConfiguration(SparkPlugMongoDbOptions.ConfigPath).ValidateDataAnnotations().ValidateOnStart();
        services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<SparkPlugMongoDbOptions>>().Value);
        // services.Configure<SparkPlugMongoDbOptions>(configuration.GetSection(SparkPlugMongoDbOptions.ConfigPath));
        var config = new SparkPlugMongoDbOptions();
        configuration.Bind(SparkPlugMongoDbOptions.ConfigPath, config);

        services.AddScoped<IMongoDbContext, MongoDbContext>();
        services.AddScoped(typeof(IRepository<,>), typeof(MongoRepository<,>));

        services.AddHealthChecks().AddMongoDbCheck("MongoDb", config.ConnectionString, tags: new[] { "mongodb", "all" });
    }

    public static void UseSparkPlugMongoDb(this WebApplication app)
    {
        // app.MapGet("/db", ([FromServices] SparkPlugMongoDbOptions options) => options);
    }
}
