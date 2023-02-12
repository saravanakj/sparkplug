namespace SparkPlug.Persistence.Abstractions;

public interface ITenantOptions<TOptions> where TOptions : new()
{
    public TOptions Value { get; }
}
