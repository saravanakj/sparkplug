namespace SparkPlug.Api.Controllers;

[ApiExplorerSettings(GroupName = "Auto Generated")]
public sealed class ApiController<TRepo, TEntity, TId> : BaseController<TRepo, TEntity, TId> where TRepo : IRepository<TId, TEntity> where TEntity : IBaseEntity<TId>, new()
{
    public ApiController(IServiceProvider serviceProvider) : base(serviceProvider) { }

    [HttpGet]
    public async Task<IEnumerable<TEntity>> List(QueryRequest request)
    {
        return await _repository.ListAsync(request);
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
