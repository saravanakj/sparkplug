namespace SparkPlug.Contracts;

public class CommandResponse<TEntity> : ApiResponse, ICommandResponse<TEntity>
{
    public CommandResponse(TEntity? data = default)
    {
        Data = data;
    }
    public TEntity? Data { get; set; }
}