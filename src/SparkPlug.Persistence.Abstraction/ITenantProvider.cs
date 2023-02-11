namespace SparkPlug.Persistence.Abstractions;

public interface ITenantProvider
{
    ITenant Resolve();
}