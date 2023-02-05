namespace SparkPlug.Api.Controllers;

[ApiExplorerSettings(GroupName = "Auto Generated")]
public class ApiController<TRepo, TEntity, TId> : BaseController<TRepo, TEntity, TId> where TRepo : IRepository<TId, TEntity> where TEntity : IBaseModel<TId>
{
    public ApiController(ILogger<ApiController<TRepo, TEntity, TId>> logger, TRepo repository) : base(logger, repository) { }

    [HttpGet]
    public async Task<IEnumerable<TEntity>> List(QueryRequest request)
    {
        var recs = await _repository.ListAsync(request);
        return recs;
    }

    [HttpPost]
    public async Task<TEntity> Post([FromBody] CommandRequest<TEntity> rec)
    {
        return await _repository.CreateAsync(rec);
    }

    [HttpGet("{id}")]
    public async Task<TEntity> Get(TId id)
    {
        return await _repository.GetAsync(id);
    }

    [HttpPut("{id}")]
    public async Task<TEntity> Put(TId id, [FromBody] CommandRequest<TEntity> rec)
    {
        return await _repository.UpdateAsync(id, rec);
    }

    [HttpPatch("{id}")]
    public async Task<TEntity> Patch(TId id, [FromBody] CommandRequest<TEntity> rec)
    {
        return await _repository.PatchAsync(id, rec);
    }

    [HttpDelete("{id}")]
    public async Task<TEntity> Delete(TId id)
    {
        return await _repository.DeleteAsync(id);
    }
}
