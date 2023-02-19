namespace SparkPlug.Contracts;

public interface IApiRequest
{
    public string[]? Deps { get; set; }
}

public interface IQueryRequest : IApiRequest
{
    string[]? Select { get; set; }
    IFilter? Where { get; set; }
    IFilter? Having { get; set; }
    string[]? Group { get; set; }
    IOrder[]? Sort { get; set; }
    IPageContext? Page { get; set; }
}

public interface ICommandRequest<TEntity> : IApiRequest
{
    TEntity? Data { get; set; }
}

public interface ICompositeRequest : IApiRequest
{
    Dictionary<string, IApiRequest>? Requests { get; set; }
}