namespace SparkPlug.Api.Configuration;

public class WebApiOptionsBuilder
{
    private readonly WebApiOptions _options;

    public WebApiOptionsBuilder(WebApiOptions options)
    {
        _options = options;
    }
    public WebApiOptionsBuilder(IOptions<WebApiOptions> options)
    {
        _options = options.Value;
    }
    public void ConfigureServices(IServiceCollection services)
    {
    }
}
