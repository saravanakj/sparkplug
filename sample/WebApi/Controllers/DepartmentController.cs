namespace SparkPlug.Sample.WebApi.Controllers;

[ApiController, Route("department"), ApiExplorerSettings(GroupName = "v1")]
public class DepartmentController : BaseController<DepartmentRepository, Department, ObjectId>
{
    public DepartmentController(ILogger<DepartmentController> logger, DepartmentRepository repository) : base(logger, repository) { }

    [HttpGet("dept/{name}")]
    public async Task<IEnumerable<Department>> GetByName(String name)
    {
        var req = new QueryRequest().Where("DepartmentName", FieldOperator.Equal, name);
        return await _repository.ListAsync(req);
    }
}