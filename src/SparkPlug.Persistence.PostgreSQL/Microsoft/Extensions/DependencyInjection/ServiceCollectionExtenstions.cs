namespace Microsoft.Extensions.DependencyInjection;

public static class SparkPlugPostgreSQLServiceCollectionExtenstions
{
    public static void AddSparkPlugPostgreSQL(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetSection(SparkPlugPostgreSqlOptions.ConfigPath);
        services.Configure<SparkPlugPostgreSqlOptions>(options);
        var config = options.Get<SparkPlugPostgreSqlOptions>() ?? throw new Exception("PostgreSql Options not configured");

        services.AddScoped<IPostgreSqlDbContext, PostgreSqlDbContext>();
        services.AddScoped(typeof(IRepository<,>), typeof(PostgreSqlRepository<,>));
        services.AddHealthChecks().AddPostgreSqlDbCheck("PostgreSqlDb", config.ConnectionString, tags: new[] { "postgresqldb", "all" });
    }

    public static void UseSparkPlugMongoDb(this IApplicationBuilder app, IServiceProvider serviceProvider)
    {
        // app.MapGet("/db", ([FromServices] SparkPlugPostgreSqlOptions options) => options);
    }
}
