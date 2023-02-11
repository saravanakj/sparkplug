namespace SparkPlug.Api.Middleware;

public class TenantResolverMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ITenant _tenant;

    public TenantResolverMiddleware(RequestDelegate next, ITenant tenant)
    {
        _next = next;
        _tenant = tenant;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // var tenantId = context.Request.Headers["TenantId"]; // Read form header
        // var tenantId = context.Request.Query["TenantId"]; // Read from QueryString
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

        var tenantId = context.GetRouteValue(SparkPlugApiConstants.Tenant)?.ToString();
        if (string.IsNullOrWhiteSpace(tenantId)) throw new Exception("Bad tenant id");
        context.Items["Tenant"] = await _tenant.GetTenantAsync(tenantId);
        // context.Request.Path = new PathString(path);
        await _next(context);
    }
}
