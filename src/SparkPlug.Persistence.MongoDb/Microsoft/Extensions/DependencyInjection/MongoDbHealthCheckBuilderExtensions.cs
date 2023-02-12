namespace Microsoft.Extensions.DependencyInjection;

public static class MongoDbHealthCheckBuilderExtensions
{
    public static IHealthChecksBuilder AddMongoDbCheck(this IHealthChecksBuilder builder, string name, string mongodbConnectionString, HealthStatus? failureStatus = default, IEnumerable<string>? tags = default, TimeSpan? timeout = default)
    {
        return builder.Add(new HealthCheckRegistration(name, _ => new MongoDbHealthCheck(mongodbConnectionString), failureStatus, tags, timeout));
    }
}