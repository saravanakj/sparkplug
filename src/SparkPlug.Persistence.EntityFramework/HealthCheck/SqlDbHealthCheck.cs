using System.Data.Common;
namespace SparkPlug.Persistence.EntityFramework.HealthCheck;

public class SqlDbHealthCheck : IHealthCheck
{
    private readonly DbConnection _connection;

    public SqlDbHealthCheck(DbConnection connection)
    {
        _connection = connection;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            await _connection.OpenAsync(cancellationToken);
            return HealthCheckResult.Healthy();
        }
        catch
        {
            return HealthCheckResult.Unhealthy();
        }
    }
}