namespace SparkPlug.Persistence.Abstractions;

public interface IRepository<TId, TEntity> // where TEntity : IBaseModel<TId>
{
    Task<IEnumerable<TEntity>> ListAsync(IQueryRequest? request);
    Task<TEntity> GetAsync(TId id);
    Task<TEntity[]> GetManyAsync(TId[] ids);
    Task<TEntity> CreateAsync(ICommandRequest<TEntity> request);
    Task<TEntity[]> CreateManyAsync(ICommandRequest<TEntity[]> request);
    Task<TEntity> UpdateAsync(TId id, ICommandRequest<TEntity> request);
    Task<TEntity> PatchAsync(TId id, ICommandRequest<TEntity> request);
    Task<TEntity> ReplaceAsync(TId id, ICommandRequest<TEntity> request);
    Task<TEntity> DeleteAsync(TId id);
    Task<long> GetCountAsync(IQueryRequest request);
}