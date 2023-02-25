namespace Microsoft.Extensions.DependencyInjection;

public static class MongoDbServiceCollectionExtenstions
{
    public static void AddMongoDb(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetSection(MongoDbOptions.ConfigPath);
        services.Configure<MongoDbOptions>(options);
        var config = options.Get<MongoDbOptions>() ?? throw new Exception("MongoDb Options not configured");
        services.AddScoped<MongoDbClient>();
        services.AddScoped(typeof(MongoRepository<,>));
        services.AddScoped<IRepositoryProvider, MongoRepositoryProvider>();
        services.AddHealthChecks().AddMongoDbCheck("MongoDb", config.ConnectionString, tags: new[] { "mongodb", "all" });
    }

    public static void UseMongoDb(this IApplicationBuilder app, IServiceProvider serviceProvider)
    {
    }
}
