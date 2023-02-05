namespace SparkPlug.Api.Abstractions.Tenant;
public interface ITenant
{
    public string? TenantId { get; set; }
    public string? Name { get; set; }
}