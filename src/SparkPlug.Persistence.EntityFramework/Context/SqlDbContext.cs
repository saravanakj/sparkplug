namespace SparkPlug.Persistence.EntityFramework.Context;

public class SqlDbContext : DbContext
{
    private readonly IModelConfigurationProvider _modelConfigurationProvider;

    public SqlDbContext(IServiceProvider serviceProvider, SqlDbContextOptions sqlOptions) : base(sqlOptions.Value)
    {
        _modelConfigurationProvider = serviceProvider.GetRequiredService<IModelConfigurationProvider>();
        Database.EnsureCreated();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _modelConfigurationProvider.Configure(modelBuilder);
    }
}
