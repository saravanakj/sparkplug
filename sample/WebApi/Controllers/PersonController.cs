namespace SparkPlug.Sample.WebApi.Controllers;

[ApiController, Route("person"), Authorize, ApiExplorerSettings(GroupName = "v1")]
public class PersonController : BaseController<PersonRepository, Person, ObjectId>
{
    public PersonController(ILogger<PersonController> logger, PersonRepository repository) : base(logger, repository) { }

    [HttpGet("name/{name}")]
    public async Task<IEnumerable<Person>> GetByName(String name)
    {
        var req = new QueryRequest().Where("PersonName", FieldOperator.Equal, name);
        return await _repository.ListAsync(req);
    }
}