namespace Microsoft.Extensions.DependencyInjection;

public static class SqlServiceCollectionExtenstions
{
    public static void AddSqlDb(this IServiceCollection services, IConfiguration configuration, Func<SqlDbOptions, SqlDbOptions>? setupAction = default)
    {
        var options = configuration.GetSection(SqlDbOptions.ConfigPath);
        services.Configure<SqlDbOptions>(options);
        var sqlOptions = options.Get<SqlDbOptions>() ?? throw new Exception("Sql Options not configured");
        services.AddScoped<SqlDbClient>();
        services.AddScoped(typeof(SqlRepository<,>));
        services.AddScoped<IRepositoryProvider, SqlRepositoryProvider>();
        services.AddMvc()
            .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        if (setupAction != null)
        {
            sqlOptions = setupAction(sqlOptions);
            if (sqlOptions.Connection == null)
            {
                throw new Exception("Connection is null");
            }
            services.AddHealthChecks().AddSqlDbCheck("SqlDb", sqlOptions.Connection, tags: new[] { "sqldb", "all" });
        }
    }

    public static void UseSqlDb(this IApplicationBuilder app, IServiceProvider serviceProvider)
    {
        // app.MapGet("/db", ([FromServices] SparkPlugEntityFrameworkOptions options) => options);
    }
}
