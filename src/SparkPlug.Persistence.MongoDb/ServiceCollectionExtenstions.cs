namespace SparkPlug.Persistence.MongoDb;

public static class SparkPlugMongoDbServiceCollectionExtenstions
{
    public static void AddSparkPlugMongoDb(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetSection(SparkPlugMongoDbOptions.ConfigPath);
        services.Configure<SparkPlugMongoDbOptions>(options);
        var config = options.Get<SparkPlugMongoDbOptions>() ?? throw new Exception("MongoDb Options not configured");
        services.AddScoped<IMongoDbContext, MongoDbContext>();
        services.AddScoped(typeof(MongoRepository<,>));
        services.AddScoped<IRepositoryProvider, MongoRepositoryProvider>();
        services.AddHealthChecks().AddMongoDbCheck("MongoDb", config.ConnectionString, tags: new[] { "mongodb", "all" });
    }

    public static void UseSparkPlugMongoDb(this IApplicationBuilder app, IServiceProvider serviceProvider)
    {
    }
}
