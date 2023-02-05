namespace SparkPlug.Contracts;

public class CommandResponse<TEntity> : ApiResponse, ICommandResponse<TEntity>
{
    public CommandResponse(string? code = null, string? message = null, TEntity? data = default) : base(code, message)
    {
        Data = data;
    }
    public TEntity? Data { get; set; }
}