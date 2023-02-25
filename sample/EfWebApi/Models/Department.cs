namespace SparkPlug.Sample.WebApi.Models;

[Api("department")]
public class Department : BaseEntity<long>
{
    public string? DepartmnetName { get; set; }
    public string? Location { get; set; }
}
