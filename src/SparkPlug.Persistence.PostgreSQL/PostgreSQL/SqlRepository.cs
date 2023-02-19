using System.Linq.Expressions;

namespace SparkPlug.Persistence.PostgreSQL;

public class SqlRepository<TId, TEntity> : IRepository<TId, TEntity> where TEntity : class, IBaseEntity<TId>, new()
{
    internal readonly SqlDbContext _context;
    internal readonly ILogger<SqlRepository<TId, TEntity>> _logger;
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
    public DbContext DbContext { get => _context.Context; }

    public SqlRepository(IServiceProvider serviceProvider)
    {
        _context = serviceProvider.GetRequiredService<SqlDbContext>();
        _logger = serviceProvider.GetRequiredService<ILogger<SqlRepository<TId, TEntity>>>();
    }

    public async Task<IEnumerable<TEntity>> ListAsync(IQueryRequest? request)
    {
        var query = DbContext.Set<TEntity>().AsQueryable();
        return await query.ToListAsync();

        // var query = DbContext.Set<TEntity>().AsQueryable();
        // if (request.Where != null)
        // {
        //     query = query.Where(request.Where.ToExpression<TEntity>());
        // }

        // if (request.Group != null && request.Group.Length > 0)
        // {
        //     query = query.GroupBy(request.Group.Join(", "), "it")
        //                  .Select("new (it.Key as Key, it as Items)");
        // }

        // if (request.Having != null)
        // {
        //     var groupByProperties = request.Group.Select(x => "Key." + x);
        //     query = query.Where(request.Having.ToExpression<Grouping<string, TEntity>>())
        //                  .Select("Items");
        // }

        // if (request.Sort != null && request.Sort.Length > 0)
        // {
        //     var orderBy = string.Join(", ", request.Sort.Select(x => x.Field + " " + x.Direction));
        //     query = query.OrderBy(orderBy);
        // }

        // if (request.Page != null)
        // {
        //     var skip = request.Page.Skip;
        //     var take = request.Page.PageSize;
        //     query = query.Skip(skip).Take(take);
        // }

        // if (request.Select != null && request.Select.Length > 0)
        // {
        //     var select = string.Join(", ", request.Select);
        //     query = query.Select($"new ({select})");
        // }

        // return await query.ToListAsync();
    }
    public async Task<(IEnumerable<TEntity>, long)> ListWithCountAsync(IQueryRequest? request)
    {
        var entitiesTask = ListAsync(request);
        var countTask = GetCountAsync(request);
        await Task.WhenAll(entitiesTask, countTask).ConfigureAwait(false);
        return (entitiesTask.Result, countTask.Result);
    }
    public async Task<TEntity> GetAsync(TId id)
    {
        var tid = id ?? throw new QueryEntityException("Id is null");
        var result = await DbSet.FindAsync(tid);
        return result ?? throw new QueryEntityException("Id is invalid");
    }
    public async Task<TEntity[]> GetManyAsync(TId[] ids)
    {
        var tids = ids ?? throw new QueryEntityException("Ids array is null or empty");
        return await DbSet.Where(x => ids.Contains(x.Id)).ToArrayAsync();
    }
    public async Task<TEntity> CreateAsync(ICommandRequest<TEntity> request)
    {
        var entity = request.Data ?? throw new CreateEntityException("Entity is null");
        var entityEntry = await DbSet.AddAsync(entity);
        return entityEntry.Entity;
    }
    public async Task<TEntity[]> CreateManyAsync(ICommandRequest<TEntity[]> requests)
    {
        var entities = requests.Data ?? throw new CreateEntityException("Entities are null");
        await DbSet.AddRangeAsync(entities);
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
        DbSet.Attach(entity);
        DbContext.Entry(entity).State = EntityState.Modified;
        await DbContext.SaveChangesAsync();
        return entity;
    }
    public async Task<TEntity> DeleteAsync(TId id)
    {
        var tid = id ?? throw new DeleteEntityException("Id is null");
        TEntity? entityToDelete = await DbSet.FindAsync(tid);
        entityToDelete = entityToDelete ?? throw new DeleteEntityException("Id is invalid");
        DbSet.Remove(entityToDelete);
        await DbContext.SaveChangesAsync();
        return entityToDelete;
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

// public static class Extention
// {
// public static IQueryable GetPageQuery(this IQueryable query, PageContext pageContext)
// {

// }
// }

// public static class FilterExtensions
// {
//     public static Expression<Func<TEntity, bool>> ToExpression<TEntity>(this IFilter filter)
//     {
//         if (filter is ICompositeFilter compositeFilter)
//         {
//             var expressions = compositeFilter.Filters?.Select(f => f.ToExpression<TEntity>());
//             var compositeExpression = expressions?.Aggregate((x, y) =>
//             {
//                 switch (compositeFilter.Op)
//                 {
//                     case CompositeOperator.And:
//                         return Expression.AndAlso(x, y);
//                     case CompositeOperator.Or:
//                         return Expression.OrElse(x, y);
//                     default:
//                         throw new NotSupportedException($"Composite operator {compositeFilter.Op} is not supported.");
//                 }
//             });

//             if (compositeExpression == null)
//             {
//                 throw new InvalidOperationException("Filters cannot be null or empty.");
//             }

//             return Expression.Lambda<Func<TEntity, bool>>(compositeExpression);
//         }
//         else if (filter is IFieldFilter fieldFilter)
//         {
//             var parameter = Expression.Parameter(typeof(TEntity), "x");
//             var member = Expression.PropertyOrField(parameter, fieldFilter.Field);
//             var constant = Expression.Constant(fieldFilter.Value);

//             switch (fieldFilter.Op)
//             {
//                 case FieldOperator.Equal:
//                     return Expression.Lambda<Func<TEntity, bool>>(Expression.Equal(member, constant), parameter);
//                 case FieldOperator.NotEqual:
//                     return Expression.Lambda<Func<TEntity, bool>>(Expression.NotEqual(member, constant), parameter);
//                 case FieldOperator.GreaterThan:
//                     return Expression.Lambda<Func<TEntity, bool>>(Expression.GreaterThan(member, constant), parameter);
//                 case FieldOperator.GreaterThanOrEqual:
//                     return Expression.Lambda<Func<TEntity, bool>>(Expression.GreaterThanOrEqual(member, constant), parameter);
//                 case FieldOperator.LessThan:
//                     return Expression.Lambda<Func<TEntity, bool>>(Expression.LessThan(member, constant), parameter);
//                 case FieldOperator.LessThanOrEqual:
//                     return Expression.Lambda<Func<TEntity, bool>>(Expression.LessThanOrEqual(member, constant), parameter);
//                 case FieldOperator.Contains:
//                     return Expression.Lambda<Func<TEntity, bool>>(Expression.Call(member, typeof(string).GetMethod("Contains", new[] { typeof(string) }), constant), parameter);
//                 case FieldOperator.StartsWith:
//                     return Expression.Lambda<Func<TEntity, bool>>(Expression.Call(member, typeof(string).GetMethod("StartsWith", new[] { typeof(string) }), constant), parameter);
//                 case FieldOperator.EndsWith:
//                     return Expression.Lambda<Func<TEntity, bool>>(Expression.Call(member, typeof(string).GetMethod("EndsWith", new[] { typeof(string) }), constant), parameter);
//                 default:
//                     throw new NotSupportedException($"Field operator {fieldFilter.Op} is not supported.");
//             }
//         }
//         else if (filter is IUnaryFilter unaryFilter)
//         {
//             var parameter = Expression.Parameter(typeof(TEntity), "x");
//             var member = Expression.PropertyOrField(parameter, unaryFilter.Field);

//             switch (unaryFilter.Op)
//             {
//                 case UnaryOperator.IsNull:
//                     return Expression.Lambda<Func<TEntity, bool>>(Expression.Equal(member, Expression.Constant(null)), parameter);
//                 case UnaryOperator.IsNotNull:
//                     return Expression.Lambda<Func<TEntity, bool>>(Expression.NotEqual(member, Expression.Constant(null)), parameter);
//                 default:
//                     throw new NotSupportedException($"Unary operator {unaryFilter.Op} is not supported.");
//             }
//         }
//         else
//         {
//             throw new NotSupportedException($"Filter type {filter.GetType().Name} is not supported.");
//         }
//     }
// }
