namespace SparkPlug.Api.Middleware;

public class TenantResolverMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ITenantResolver _tenantResolver;

    public TenantResolverMiddleware(RequestDelegate next, ITenantResolver tenantResolver)
    {
        _next = next;
        _tenantResolver = tenantResolver;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // var tenantId = context.Request.Headers["TenantId"]; // Read form header
        // var tenantId = context.Request.Query["TenantId"]; // Read from QueryString
        var tenantId = context.GetRouteValue("tenantId")?.ToString();

        // Get Tenent Id from Payload message.
        // string tenantId = null;
        // if (context.Request.Method == HttpMethod.Post.Method || context.Request.Method == HttpMethod.Put.Method)
        // {
        //     string requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();
        //     dynamic data = JsonConvert.DeserializeObject(requestBody);
        //     tenantId = data?.TenantId;
        // }
        // if (!string.IsNullOrEmpty(tenantId))
        // {
        //     context.Items["TenantId"] = tenantId;
        // }

        context.Items["Tenant"] = _tenantResolver.ResolveAsync(tenantId);
        // context.Request.Path = new PathString(path);
        await _next(context);
    }
}
