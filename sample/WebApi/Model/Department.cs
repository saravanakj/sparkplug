namespace SparkPlug.Sample.WebApi.Models;

[Collection("Departments")]
public class Department : BaseModel<ObjectId>
{
    public string? DepartmnetName { get; set; }
    public string? Location { get; set; }
}
