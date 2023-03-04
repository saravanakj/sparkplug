namespace SparkPlug.Persistence.EntityFramework.Models;

public class TenantDetails : BaseEntity<Guid>, IConcurrencyEntity
{
    public string? Name { get; set; }
    public List<Options> Options { get; set; } = new List<Options>();
    public int Revision { get; set; }
}

public class Options : BaseEntity<Guid>, IConcurrencyEntity
{
    public string Key { get; set; } = string.Empty;
    public string? Value { get; set; }
    public int Revision { get; set; }
}