namespace SparkPlug.Sample.WebApi.Models;

[Api("department")]
public class Department : BaseEntity<long>, IConcurrencyEntity, IAuditableEntity<long>
{
    public string? DepartmnetName { get; set; }
    public string? Location { get; set; }
    [ConcurrencyCheck]
    public int Revision { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CreatedBy { get; set; }
    public DateTime ModifiedAt { get; set; }
    public long ModifiedBy { get; set; }
}
