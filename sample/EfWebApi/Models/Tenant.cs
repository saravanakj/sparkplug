namespace SparkPlug.Sample.WebApi.Models;

[Api("tenants")]
public class TenantDetails : BaseEntity<Guid>
{
    public string? Name { get; set; }
    public List<Options> Options { get; set; } = new List<Options>();
}

public class Options
{
    public string Key { get; set; } = string.Empty;
    public string? Value { get; set; }
}