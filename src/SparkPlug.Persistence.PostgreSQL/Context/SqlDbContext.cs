namespace SparkPlug.Persistence.PostgreSQL.Context;

public class SqlDbContext : ISqlDbContext
{
    public DbContext Context { get; }
    public SqlDbContext(ITenantOptions<TenantConfig> options)
    {
        Context = GetClient(options.Value.ConnectionString);
    }
    // public DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class
    // {
    //     return Context.Set<TEntity>();
    // }
    public DbContext GetClient(string connectionString)
    {
        var options = new DbContextOptionsBuilder()
               .UseNpgsql(connectionString)
               .Options;
        return new DbContext(options);
    }
    // public int Save()
    // {
    //     return Context.SaveChanges();
    // }
    // public async Task<int> SaveAsync()
    // {
    //     return await Context.SaveChangesAsync();
    // }
}
