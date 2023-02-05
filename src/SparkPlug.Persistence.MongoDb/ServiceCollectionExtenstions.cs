namespace SparkPlug.Persistence.MongoDb;

public static class SparkPlugMongoDbServiceCollectionExtenstions
{
    public static void AddSparkPlugMongoDb(this IServiceCollection services)
    {
        services.AddOptions<SparkPlugMongoDbOptions>().BindConfiguration(SparkPlugMongoDbOptions.ConfigPath).ValidateDataAnnotations().ValidateOnStart();
        services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<SparkPlugMongoDbOptions>>().Value);
        services.AddSingleton<IMongoDbContext, MongoDbContext>();
        services.AddHealthChecks().AddMongoDb("MongoDb", "", tags: new[] { "mongodb", "all" });
    }

    public static void UseSparkPlugMongoDb(this WebApplication app)
    {
        // app.MapGet("/db", ([FromServices] SparkPlugMongoDbOptions options) => options);
    }
}
