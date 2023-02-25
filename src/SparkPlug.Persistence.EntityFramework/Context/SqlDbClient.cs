namespace SparkPlug.Persistence.EntityFramework.Context;

public class SqlDbClient
{
    public SqlDbContext Context { get; }
    private readonly IServiceProvider _serviceProvider;
    private readonly IDbContextOptionsProvider _dbContextOptionsProvider;

    public SqlDbClient(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _dbContextOptionsProvider = serviceProvider.GetRequiredService<IDbContextOptionsProvider>();
        var tenantConfig = serviceProvider.GetRequiredService<ITenantOptions<TenantConfig>>();
        Context = GetClient(tenantConfig.Value.ConnectionString);
        // TODO: Need to check is this right place.
        Context.Database.EnsureCreated();
    }
    public SqlDbContext GetClient(string connectionString)
    {
        var options = _dbContextOptionsProvider.GetDbContextOption(connectionString);
        return new SqlDbContext(_serviceProvider, options);
    }
}
