namespace SparkPlug.Contracts;

public class QueryResponse<TEntity> : ApiResponse, IQueryResponse<TEntity>
{
    public QueryResponse(TEntity[]? data = default, IPageContext? pc = null)
    {
        Data = data;
        Page = pc;
    }
    public IPageContext? Page { get; set; }
    public TEntity[]? Data { get; set; }
}

public static partial class Extensions
{
    #region QueryResponse
    public static IQueryResponse<TEntity> AddResponse<TEntity>(this IQueryResponse<TEntity> source, TEntity data)
    {
        return source.AddResponse(new TEntity[] { data });
    }
    public static IQueryResponse<TEntity> AddResponse<TEntity>(this IQueryResponse<TEntity> source, TEntity[] data)
    {
        source.Data = source.Data?.Concat(data).ToArray() ?? data;
        return source;
    }
    public static IQueryResponse<TEntity> AddPageContext<TEntity>(this IQueryResponse<TEntity> source, IPageContext pc)
    {
        source.Page = pc;
        return source;
    }
    #endregion
}