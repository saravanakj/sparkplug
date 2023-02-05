namespace SparkPlug.Persistence.PostgreSQL.HealthCheck;

public class PostgreSqlDbHealthCheck : IHealthCheck
{
    private readonly string _connectionString;

    public PostgreSqlDbHealthCheck(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        try
        {
            await connection.OpenAsync(cancellationToken);
            return HealthCheckResult.Healthy();
        }
        catch
        {
            return HealthCheckResult.Unhealthy();
        }
    }
}