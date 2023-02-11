namespace SparkPlug.Sample.WebApi.Controllers;

[ApiController, Route("person"), Authorize, ApiExplorerSettings(GroupName = "v1")]
public class PersonController : BaseController<Repository<String, Person>, Person, String>
{
    public PersonController(IServiceProvider serviceProvider) : base(serviceProvider) { }

    [HttpGet("name/{name}")]
    public async Task<IEnumerable<Person>> GetByName(String name)
    {
        var req = new QueryRequest().Where("PersonName", FieldOperator.Equal, name);
        return await _repository.ListAsync(req);
    }
}