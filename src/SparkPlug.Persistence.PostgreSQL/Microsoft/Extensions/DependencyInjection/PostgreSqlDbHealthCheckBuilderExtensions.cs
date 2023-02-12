namespace Microsoft.Extensions.DependencyInjection;

public static class PostgreSqlDbHealthCheckBuilderExtensions
{
    public static IHealthChecksBuilder AddPostgreSqlDbCheck(this IHealthChecksBuilder builder, string name, string postgreSqlDbConnectionString, HealthStatus? failureStatus = default, IEnumerable<string>? tags = default, TimeSpan? timeout = default)
    {
        return builder.Add(new HealthCheckRegistration(name, _ => new PostgreSqlDbHealthCheck(postgreSqlDbConnectionString), failureStatus, tags, timeout));
    }
}