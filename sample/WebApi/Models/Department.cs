namespace SparkPlug.Sample.WebApi.Models;

[Collection("Departments")]
[Api("department")]
public class Department : BaseEntity<ObjectId>
{
    public string? DepartmnetName { get; set; }
    public string? Location { get; set; }
}
