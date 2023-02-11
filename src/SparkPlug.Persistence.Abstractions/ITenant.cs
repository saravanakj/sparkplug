namespace SparkPlug.Persistence.Abstractions;

public interface ITenant
{
    Task<ITenantOptions> GetTenantAsync(string id);
}