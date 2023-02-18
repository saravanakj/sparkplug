namespace SparkPlug.Persistence.PostgreSQL.Context;

public interface ISqlDbContext : IDbContext<DbContext>
{
    // public DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class;
    // public int Save();
    // public Task<int> SaveAsync();
}
