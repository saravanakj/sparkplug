namespace SparkPlug.Sample.WebApi.Controllers;

[ApiController, Route("person"), ApiExplorerSettings(GroupName = "v1")]
public class PersonController : BaseController<Repository<string, Person>, Person, string>
{
    public PersonController(IServiceProvider serviceProvider) : base(serviceProvider) { }

    [HttpGet("name/{name}")]
    public async Task<IActionResult> GetByName(String name)
    {
        var req = new QueryRequest().Where("PersonName", FieldOperator.Equal, name);
        var entities = await _repository.ListAsync(req);
        return Ok(entities);
    }

    [HttpGet("pathc-request-object")]
    public async Task<IActionResult> GetPatchReqestObject()
    {
        var patchDoc = new JsonPatchDocument<Person>();
        patchDoc.Replace(p => p.PersonName, "John Doe");
        patchDoc.Replace(p => p.Salary, 30000000);
        var req = new CommandRequest<JsonPatchDocument<Person>>(patchDoc);
        var response = await Task.FromResult(req);
        return Ok(response);
    }
}