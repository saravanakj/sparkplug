namespace SparkPlug.Contracts;

public class CompositeRequest : ApiRequest, ICompositeRequest
{
    public CompositeRequest()
    {
        Requests = new Dictionary<string, IApiRequest>();
    }
    public Dictionary<string, IApiRequest>? Requests { get; set; }
}

public static partial class Extensions
{
    #region CompositeRequest
    public static ICompositeRequest Add(this ICompositeRequest source, string key, IApiRequest value)
    {
        (source.Requests ??= new Dictionary<string, IApiRequest>()).Add(key, value);
        return source;
    }
    #endregion
}