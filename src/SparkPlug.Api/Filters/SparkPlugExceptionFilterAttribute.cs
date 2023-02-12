namespace SparkPlug.Api.Filters;

public class SparkPlugExceptionFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var response = context.HttpContext.Response;
        response.StatusCode = (int)HttpStatusCode.InternalServerError;
        response.ContentType = SparkPlugApiConstants.ContentType;
        context.Result = new JsonResult(new ErrorResponse("Internal Server Error", context.Exception));
        context.ExceptionHandled = true;
    }
}
