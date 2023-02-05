namespace SparkPlug.Api.Controllers;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ApiAttribute : Attribute
{
    public ApiAttribute(string route)
    {
        Route = route;
    }

    public string Route { get; set; }
}
