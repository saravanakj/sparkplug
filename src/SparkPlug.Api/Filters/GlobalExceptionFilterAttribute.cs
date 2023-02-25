namespace SparkPlug.Api.Filters;

public class GlobalExceptionFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var response = context.HttpContext.Response;
        response.StatusCode = (int)HttpStatusCode.InternalServerError;
        response.ContentType = WebApiConstants.ContentType;
        context.Result = new JsonResult(new ErrorResponse("Internal Server Error", context.Exception));
        context.ExceptionHandled = true;
    }
}
