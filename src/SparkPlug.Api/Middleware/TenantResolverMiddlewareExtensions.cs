namespace SparkPlug.Api.Middleware;

public static class TenantResolverMiddlewareExtensions
{
    public static IApplicationBuilder UseTenantResolverMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TenantResolverMiddleware>();
    }
}
