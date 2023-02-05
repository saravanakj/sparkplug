
namespace SparkPlug.Api.Abstractions.Tenant;

public interface ITenantResolver
{
    Task<ITenant> ResolveAsync(string? tenantId);
}