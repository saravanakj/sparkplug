namespace SparkPlug.Persistence.Abstractions;
public static class RepositoryExtentions
{
    public static TEntity Auditable<TId, TEntity>(this TEntity entity, object userId, DateTime currentTime, bool isCreate = false) where TEntity : class, IBaseEntity<TId>
    {
        if (entity is IAuditableEntity obj)
        {
            if (isCreate)
            {
                obj.CreatedAt = currentTime;
                obj.CreatedBy = userId;
            }
            obj.ModifiedAt = DateTime.UtcNow;
            obj.ModifiedBy = currentTime;
        }
        return entity;
    }

    public static TEntity Deletable<TId, TEntity>(this TEntity entity) where TEntity : class, IBaseEntity<TId>
    {
        if (entity is IDeletableEntity obj)
        {
            obj.Status = Status.Live;
        }
        return entity;
    }
}