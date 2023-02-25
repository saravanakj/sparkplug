namespace SparkPlug.Persistence.EntityFramework.Context;

public class SqlDbContext : DbContext
{
    private readonly IModelBuilderProvider _modelBuilderProvider;

    public SqlDbContext(IServiceProvider serviceProvider, DbContextOptions options) : base(options)
    {
        _modelBuilderProvider = serviceProvider.GetRequiredService<IModelBuilderProvider>();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _modelBuilderProvider.Build(modelBuilder);
    }
}
