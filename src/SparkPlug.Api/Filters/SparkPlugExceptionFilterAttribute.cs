namespace SparkPlug.Api.Filters;

public class SparkPlugExceptionFilterAttribute : ExceptionFilterAttribute
{
    const int _statusCode = 500;
    const string _errorMessage = "Internal Server Error";
    public override void OnException(ExceptionContext context)
    {
        var response = context.HttpContext.Response;
        response.StatusCode = _statusCode;
        response.ContentType = "application/json";
        context.Result = new JsonResult(new ErrorResponse(_errorMessage, context.Exception));
        context.ExceptionHandled = true;
    }
}
