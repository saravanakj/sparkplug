namespace SparkPlug.Persistence.PostgreSQL;

public class PostgreSqlRepository<TId, TEntity> : IRepository<TId, TEntity> where TEntity : IBaseEntity<TId>, new()
{
    public Task<TEntity> CreateAsync(ICommandRequest<TEntity> request)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity[]> CreateManyAsync(ICommandRequest<TEntity[]> requests)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> DeleteAsync(TId id)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> GetAsync(TId id)
    {
        throw new NotImplementedException();
    }

    public Task<long> GetCountAsync(IQueryRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity[]> GetManyAsync(TId[] ids)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TEntity>> ListAsync(IQueryRequest? request)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> PatchAsync(TId id, ICommandRequest<TEntity> request)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> ReplaceAsync(TId id, ICommandRequest<TEntity> request)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> UpdateAsync(TId id, ICommandRequest<TEntity> request)
    {
        throw new NotImplementedException();
    }
}