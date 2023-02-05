namespace SparkPlug.Persistence.PostgreSQL.Context;

public class PostgreSqlDbContext : IPostgreSqlDbContext
{
    public DbContext GetClient(string connectionString)
    {
         var options = new DbContextOptionsBuilder()
                .UseNpgsql(connectionString)
                .Options;
        return new DbContext(options);
    }
}
