namespace SparkPlug.Persistence.Abstractions;

public class Repository<TId, TEntity> : IRepository<TId, TEntity> where TEntity : class, IBaseEntity<TId>, new()
{
    private readonly IRepository<TId, TEntity> _repository;
    private readonly IServiceProvider _serviceProvider;

    public Repository(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        var repositoryProvider = _serviceProvider.GetRequiredService<IRepositoryProvider>();
        _repository = repositoryProvider.GetRepository<TId, TEntity>();
    }
    public IRepository<TId, TEntity> GetRepository() => _repository;
    public TService GetService<TService>() where TService : class
            => _serviceProvider.GetRequiredService<TService>();

    public Task<TEntity> CreateAsync(ICommandRequest<TEntity> request)
    {
        return _repository.CreateAsync(request);
    }

    public Task<TEntity[]> CreateManyAsync(ICommandRequest<TEntity[]> request)
    {
        return _repository.CreateManyAsync(request);
    }

    public Task<TEntity> DeleteAsync(TId id)
    {
        return _repository.DeleteAsync(id);
    }

    public Task<TEntity> GetAsync(TId id)
    {
        return _repository.GetAsync(id);
    }

    public Task<long> GetCountAsync(IQueryRequest? request)
    {
        return _repository.GetCountAsync(request);
    }

    public Task<TEntity[]> GetManyAsync(TId[] ids)
    {
        return _repository.GetManyAsync(ids);
    }

    public Task<IEnumerable<TEntity>> ListAsync(IQueryRequest? request)
    {
        return _repository.ListAsync(request);
    }
     public async Task<(IEnumerable<TEntity>, long)> ListWithCountAsync(IQueryRequest? request)
     {
        return await _repository.ListWithCountAsync(request);
     }

    public Task<TEntity> PatchAsync(TId id, ICommandRequest<JsonPatchDocument<TEntity>> request)
    {
        return _repository.PatchAsync(id, request);
    }

    public Task<TEntity> ReplaceAsync(TId id, ICommandRequest<TEntity> request)
    {
        return _repository.ReplaceAsync(id, request);
    }

    public Task<TEntity> UpdateAsync(TId id, ICommandRequest<TEntity> request)
    {
        return _repository.UpdateAsync(id, request);
    }
}