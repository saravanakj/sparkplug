namespace SparkPlug.Persistence.Abstractions;

public class Tenant : ITenant
{
    private readonly ITenant _tenant;
    public Tenant(IServiceProvider serviceProvider)
    {
        var tenantProvider = serviceProvider.GetRequiredService<ITenantProvider>();
        _tenant = tenantProvider.Resolve();
    }

    public async Task<ITenantOptions> GetTenantAsync(string id)
    {
        return await _tenant.GetTenantAsync(id);
    }
}