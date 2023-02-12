namespace SparkPlug.Persistence.Abstractions;

public interface ITenantOptions<TOptions> where TOptions : new()
{
    public TOptions Value { get; }
}

public class TenantOptionsManager<TOptions> : ITenantOptions<TOptions> where TOptions : new()
{
    private IConfiguration configuration = null!;
    private readonly ITenant _tenant;
    public TOptions Value { get; }
    public TenantOptionsManager(ITenant tenant)
    {
        _tenant = tenant;
        Value = GetOptions();
    }

    public TOptions GetOptions()
    {
        configuration ??= new ConfigurationBuilder()
            .AddInMemoryCollection(_tenant.Options)
            .Build();
        return configuration.GetSection(typeof(TOptions).Name).Get<TOptions>() ?? new TOptions();
    }
}
