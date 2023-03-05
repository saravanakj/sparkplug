namespace SparkPlug.Contracts;

[JsonConverter(typeof(FilterConverter))]
public class Filter : JObject, IFilter
{
    public Filter(FilterType filterType)
    {
        FilterType = filterType;
    }
    public FilterType FilterType { get; set; }
}
