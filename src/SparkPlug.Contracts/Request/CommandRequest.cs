namespace SparkPlug.Contracts;

public class CommandRequest<TEntity> : ApiRequest, ICommandRequest<TEntity>
{
    public CommandRequest() { }
    public CommandRequest(TEntity? data = default)
    {
        Data = data;
    }
    public TEntity? Data { get; set; }
}