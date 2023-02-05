namespace SparkPlug.Persistence.PostgreSQL;

public static class SparkPlugPostgreSQLServiceCollectionExtenstions
{
    public static void AddSparkPlugPostgreSQL(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<SparkPlugPostgreSqlOptions>().BindConfiguration(SparkPlugPostgreSqlOptions.ConfigPath).ValidateDataAnnotations().ValidateOnStart();
        services.Configure<SparkPlugPostgreSqlOptions>(configuration.GetSection(SparkPlugPostgreSqlOptions.ConfigPath));
        var config = configuration.GetValue<SparkPlugPostgreSqlOptions>(SparkPlugPostgreSqlOptions.ConfigPath);
        services.AddSingleton<IPostgreSqlDbContext, PostgreSqlDbContext>();
        services.AddScoped(typeof(IRepository<,>), typeof(PostgreSqlRepository<,>));
        services.AddHealthChecks().AddPostgreSqlDbCheck("PostgreSqlDb", config!.ConnectionString, tags: new[] { "postgresqldb", "all" });
    }

    public static void UseSparkPlugMongoDb(this WebApplication app)
    {
        // app.MapGet("/db", ([FromServices] SparkPlugPostgreSqlOptions options) => options);
    }
}
