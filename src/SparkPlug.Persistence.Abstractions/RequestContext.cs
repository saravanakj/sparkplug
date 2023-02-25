namespace SparkPlug.Persistence.Abstractions;

public class RequestContext : IRequestContext
{
    public static RequestContext Default { get => new(); }
    public string UserId { get; set; } = string.Empty;
}
