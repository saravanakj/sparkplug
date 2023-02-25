namespace Microsoft.Extensions.DependencyInjection;

public static class SqlDbHealthCheckBuilderExtensions
{
    public static IHealthChecksBuilder AddSqlDbCheck(this IHealthChecksBuilder builder, string name, DbConnection connection, HealthStatus? failureStatus = default, IEnumerable<string>? tags = default, TimeSpan? timeout = default)
    {
        return builder.Add(new HealthCheckRegistration(name, _ => new SqlDbHealthCheck(connection), failureStatus, tags, timeout));
    }
}
