namespace SparkPlug.Persistence.Abstractions;

public interface ITenantOptions
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string ConnectionString { get; set; }
}