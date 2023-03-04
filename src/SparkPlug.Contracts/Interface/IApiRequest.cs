namespace SparkPlug.Contracts;

public interface IApiRequest
{
    public string[]? Deps { get; set; }
}

public interface IQueryRequest : IApiRequest
{
    string[]? Select { get; set; }
    Filter? Where { get; set; }
    Order[]? Sort { get; set; }
    PageContext? Page { get; set; }
}

public interface ICommandRequest<TEntity> : IApiRequest
{
    TEntity? Data { get; set; }
}

public interface ICompositeRequest : IApiRequest
{
    Dictionary<string, IApiRequest>? Requests { get; set; }
}