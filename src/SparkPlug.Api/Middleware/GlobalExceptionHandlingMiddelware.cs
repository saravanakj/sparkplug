namespace SparkPlug.Api.Middleware;

public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var error = new ErrorResponse("Internal Server Error", exception);
        _logger.LogError("Global Exception - ", error.Message);
        var response = context.Response;
        response.StatusCode = StatusCodes.Status500InternalServerError;
        response.ContentType = WebApiConstants.ContentType;
        await context.Response.WriteAsJsonAsync(new JsonResult(error));
    }
}
