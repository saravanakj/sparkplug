namespace SparkPlug.Persistence.EntityFramework;

public class SqlRepository<TId, TEntity> : IRepository<TId, TEntity> where TEntity : class, IBaseEntity<TId>, new()
{
    public SqlDbContext DbContext { get; }
    internal readonly ILogger<SqlRepository<TId, TEntity>> logger;
    internal readonly IRequestContext<TId> requestContext;
    private DbSet<TEntity>? _dbSet;
    public virtual DbSet<TEntity> DbSet
    {
        get
        {
            return _dbSet ??= GetDbSet();
        }
    }
    public DbSet<TEntity> GetDbSet()
    {
        return DbContext.Set<TEntity>();
    }
    public DbSet<Entity> GetDbSet<Entity>() where Entity : class, IBaseEntity<TId>, new()
    {
        return DbContext.Set<Entity>();
    }
    public SqlRepository(IServiceProvider serviceProvider)
    {
        DbContext = serviceProvider.GetRequiredService<SqlDbContext>();
        logger = serviceProvider.GetRequiredService<ILogger<SqlRepository<TId, TEntity>>>();
        requestContext = serviceProvider.GetRequiredService<IRequestContext<TId>>();
    }
    public async Task<(IEnumerable<TEntity>, long)> ListWithCountAsync(IQueryRequest? request)
    {
        var query = GetQuery(request);
        return (await query.ToListAsync(), await query.LongCountAsync());
    }
    public async Task<IEnumerable<TEntity>> ListAsync(IQueryRequest? request)
    {
        return await GetQuery(request).ToListAsync();
    }

    public IQueryable<TEntity> GetQuery(IQueryRequest? request)
    {
        var query = GetDbSet().AsQueryable();
        var TEntityType = typeof(TEntity);
        var ObjectType = typeof(object);
        if (request?.Select != null && request.Select.Any())
        {
            var properties = TEntityType.GetProperties().Where(prop => request.Select.Contains(prop.Name));
            var parameter = Expression.Parameter(TEntityType, "entity");
            var propertyBindings = properties.Select(property => Expression.Bind(property, Expression.MakeMemberAccess(parameter, property)));
            var selector = Expression.Lambda<Func<TEntity, TEntity>>(Expression.MemberInit(Expression.New(TEntityType), propertyBindings), parameter);
            query = query.Select(selector);
        }
        if (request?.Sort?.Length > 0)
        {
            var orderedQuery = query.OrderBy(_ => 0);
            foreach (var order in request.Sort)
            {
                var parameterExp = Expression.Parameter(TEntityType, "entity");
                var propertyExp = Expression.Property(parameterExp, order.Field);
                var lambdaExp = Expression.Lambda<Func<TEntity, object>>(Expression.Convert(propertyExp, typeof(object)), parameterExp);
                orderedQuery = order.Direction == Direction.Ascending ? orderedQuery.ThenBy(lambdaExp) : orderedQuery.ThenByDescending(lambdaExp);
            }
            query = orderedQuery;
        }
        return request?.Where == null ? query : query.Where(request.Where.GetFilterDefinition<TEntity>());
    }
    public async Task<TEntity> GetAsync(TId id)
    {
        var tid = id ?? throw new QueryEntityException("Id is null");
        var result = await DbSet.FindAsync(tid).ConfigureAwait(false);
        return result ?? throw new QueryEntityException("Id is not found");
    }
    public async Task<TEntity[]> GetManyAsync(TId[] ids)
    {
        var tids = ids ?? throw new QueryEntityException("Ids are null or empty");
        return await DbSet.Where(x => ids.Contains(x.Id)).ToArrayAsync();
    }
    public async Task<TEntity> CreateAsync(ICommandRequest<TEntity> request)
    {
        var entity = request.Data ?? throw new CreateEntityException("Entity is null");
        entity = entity.Auditable<TId, TEntity>(requestContext.UserId, DateTime.UtcNow);
        var entityEntry = await DbSet.AddAsync(entity);
        await DbContext.SaveChangesAsync();
        return entityEntry.Entity;
    }
    public async Task<TEntity[]> CreateManyAsync(ICommandRequest<TEntity[]> requests)
    {
        var entities = requests.Data ?? throw new CreateEntityException("Entities are null");
        await DbSet.AddRangeAsync(entities.Select(x => x.Auditable<TId, TEntity>(requestContext.UserId, DateTime.UtcNow, true)));
        await DbContext.SaveChangesAsync();
        return entities;
    }
    public async Task<TEntity> UpdateAsync(TId id, ICommandRequest<TEntity> request)
    {
        var entity = request.Data ?? throw new UpdateEntityException("Entity is null");
        entity.Id = id ?? throw new UpdateEntityException("Id is null");
        return await UpdateAsync(entity);
    }
    private async Task<TEntity> UpdateAsync(TEntity entity)
    {
        entity = entity.Auditable<TId, TEntity>(requestContext.UserId, DateTime.UtcNow);
        DbSet.Attach(entity);
        if (entity is IConcurrencyEntity obj) { obj.Revision++; }
        DbContext.Entry(entity).State = EntityState.Modified;
        await DbContext.SaveChangesAsync();
        return entity;
    }
    public async Task<TEntity> DeleteAsync(TId id)
    {
        var tid = id ?? throw new DeleteEntityException("Id is null");
        TEntity entityToDelete = (await DbSet.FindAsync(tid)) ?? throw new DeleteEntityException("Id is invalid");
        entityToDelete = entityToDelete.Deletable<TId, TEntity>();
        return await UpdateAsync(entityToDelete);
    }
    public async Task<long> GetCountAsync(IQueryRequest? request)
    {
        return await DbSet.LongCountAsync();
    }
    public async Task<TEntity> PatchAsync(TId id, ICommandRequest<JsonPatchDocument<TEntity>> request)
    {
        var patchDocument = request.Data ?? throw new UpdateEntityException("Entity is null");
        var tid = id ?? throw new UpdateEntityException("Id is null");
        TEntity original = await GetAsync(tid);
        // patchDocument.Operations.Add(new Operation<TEntity>("replace", $"/{nameof(IAuditableEntity.ModifiedBy)}", null, _user.UserId));
        // patchDocument.Operations.Add(new Operation<TEntity>("replace", $"/{nameof(IAuditableEntity.ModifiedAt)}", null, DateTime.UtcNow));
        patchDocument.ApplyTo(original);
        return await UpdateAsync(original);
    }
    public async Task<TEntity> ReplaceAsync(TId id, ICommandRequest<TEntity> request)
    {
        var entity = request.Data ?? throw new UpdateEntityException("Entity is null");
        var tid = id ?? throw new UpdateEntityException("Id is null");
        var sourceEntity = await GetAsync(tid);
        sourceEntity = sourceEntity ?? throw new UpdateEntityException("Id is invalid");
        DbContext.Entry(sourceEntity).CurrentValues.SetValues(entity);
        var modifyedCount = await DbContext.SaveChangesAsync();
        if (modifyedCount == 0) throw new UpdateEntityException("No records are replaced");
        return sourceEntity;
    }
}

public static class Extention
{
    public static IEnumerable<Expression<Func<TEntity, bool>>> GetFilterDefinitions<TEntity>(this IFilter[] filters)
    {
        return filters.Select(x => x.GetFilterDefinition<TEntity>()).ToArray();
    }

    public static Expression<Func<TEntity, bool>> GetFilterDefinition<TEntity>(this IFilter filter)
    {
        return filter switch
        {
            ICompositeFilter compositeFilter => compositeFilter.GetFilterDefinition<TEntity>(),
            IFieldFilter fieldFilter => fieldFilter.GetFilterDefinition<TEntity>(),
            IUnaryFilter unaryFilter => unaryFilter.GetFilterDefinition<TEntity>(),
            _ => throw new NotSupportedException($"Filter type {filter.GetType().Name} is not supported")
        };
    }

    public static Expression<Func<TEntity, bool>> GetFilterDefinition<TEntity>(this ICompositeFilter compositeFilter)
    {
        var parameter = Expression.Parameter(typeof(TEntity), "entity");
        var expression = compositeFilter.Op switch
        {
            CompositeOperator.And => compositeFilter.Filters?.GetFilterDefinitions<TEntity>().Aggregate((a, b) => Expression.Lambda<Func<TEntity, bool>>(Expression.AndAlso(a, b), parameter)),
            CompositeOperator.Or => compositeFilter.Filters?.GetFilterDefinitions<TEntity>().Aggregate((a, b) => Expression.Lambda<Func<TEntity, bool>>(Expression.AndAlso(a, b), parameter)),
            _ => throw new QueryEntityException("Invalid composite filter operation")
        };
        Expression<Func<TEntity, bool>> p = (_) => true;
        return expression ?? p;
    }

    public static Expression<Func<TEntity, bool>> GetFilterDefinition<TEntity>(this IFieldFilter fieldFilter)
    {
        var parameter = Expression.Parameter(typeof(TEntity), "entity");
        var left = Expression.Property(parameter, fieldFilter.Field);
        var right = Expression.Constant(fieldFilter.Value);
        Expression body = fieldFilter.Op switch
        {
            FieldOperator.Equal => Expression.Equal(left, right),
            FieldOperator.NotEqual => Expression.NotEqual(left, right),
            FieldOperator.GreaterThan => Expression.GreaterThan(left, right),
            FieldOperator.GreaterThanOrEqual => Expression.GreaterThanOrEqual(left, right),
            FieldOperator.LessThan => Expression.LessThan(left, right),
            FieldOperator.LessThanOrEqual => Expression.LessThanOrEqual(left, right),
            FieldOperator.In => Expression.Call(Expression.Constant(fieldFilter.Value), "Contains", null, left),
            FieldOperator.NotIn => Expression.Not(Expression.Call(Expression.Constant(fieldFilter.Value), "Contains", null, left)),
            _ => throw new QueryEntityException("Invalid field filter operation")
        };
        return Expression.Lambda<Func<TEntity, bool>>(body, parameter);
    }

    public static Expression<Func<TEntity, bool>> GetFilterDefinition<TEntity>(this IUnaryFilter unaryFilter)
    {
        var parameter = Expression.Parameter(typeof(TEntity), "entity");
        var left = Expression.Property(parameter, unaryFilter.Field);
        var right = Expression.Constant(unaryFilter.Op == UnaryOperator.IsNull ? null : DBNull.Value);
        var body = unaryFilter.Op == UnaryOperator.IsNull ? Expression.Equal(left, right) : Expression.NotEqual(left, right);
        return Expression.Lambda<Func<TEntity, bool>>(body, parameter);
    }
}
