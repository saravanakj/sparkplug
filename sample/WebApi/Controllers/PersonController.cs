namespace SparkPlug.Sample.WebApi.Controllers;

[ApiController, Route("person"), ApiExplorerSettings(GroupName = "v1")]
public class PersonController : BaseController<Repository<string, Person>, Person, string>
{
    public PersonController(IServiceProvider serviceProvider) : base(serviceProvider) { }

    [HttpGet("name/{name}")]
    public async Task<IEnumerable<Person>> GetByName(String name)
    {
        var req = new QueryRequest().Where("PersonName", FieldOperator.Equal, name);
        return await _repository.ListAsync(req);
    }

    [HttpGet("pathc-request-object")]
    public async Task<CommandRequest<JsonPatchDocument<Person>>> GetPatchReqestObject()
    {
        var patchDoc = new JsonPatchDocument<Person>();
        patchDoc.Replace(p => p.PersonName, "John Doe");
        patchDoc.Replace(p => p.Salary, 30000000);
        var req = new CommandRequest<JsonPatchDocument<Person>>(patchDoc);
        return await Task.FromResult(req);
    }
}