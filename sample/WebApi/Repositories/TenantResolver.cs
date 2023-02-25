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
        var options = _serviceProvider.GetRequiredService<IOptions<MongoDbOptions>>().Value;
        var dict = new Dictionary<string, string?>()
        {
            { $"{nameof(TenantConfig)}:{nameof(options.ConnectionString)}", options.ConnectionString }
        };
        var result = new Tenant() { Options = dict };
        if (!string.IsNullOrWhiteSpace(id))
        {
            if (!ObjectId.TryParse(id, out ObjectId oid)) { throw new ArgumentException("${id} is not valid Tenant Id"); }
            var repo = _serviceProvider.GetRequiredService<Repository<ObjectId, TenantDetails>>();
            var tenantDetails = await repo.GetAsync(oid);
            tenantDetails.Options.ForEach(x => dict[x.Key] = x.Value);
            result = new Tenant() { Id = tenantDetails.Id.ToString(), Name = tenantDetails.Name, Options = dict };
        }
        return result;
    }
}
