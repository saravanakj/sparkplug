namespace SparkPlug.Sample.WebApi.Repositories;

public class TenantResolver : ITenantResolver
{
    private readonly IServiceProvider _serviceProvider;
    public TenantResolver(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<ITenant> ResolveAsync(string? id)
    {
        var options = _serviceProvider.GetRequiredService<IOptions<SparkPlugMongoDbOptions>>().Value;
        var dict = new Dictionary<string, string?>()
        {
            { $"{nameof(TenantConfig)}:{nameof(options.ConnectionString)}", options.ConnectionString }
        };
        var result = new Tenant() { Options = dict };
        if (!string.IsNullOrWhiteSpace(id))
        {
            var repo = _serviceProvider.GetRequiredService<Repository<string, TenantDetails>>();
            var tenantDetails = await repo.GetAsync(id);
            tenantDetails.Options.ForEach(x => dict[x.Key] = x.Value);
            result = new Tenant() { Id = tenantDetails.Id, Name = tenantDetails.Name, Options = dict };
        }
        return result;
    }
}
