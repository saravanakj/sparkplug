namespace SparkPlug.Contracts;

public class ErrorResponse : ApiResponse, IErrorResponse
{
    public ErrorResponse(string? code = null, string? message = null, string? stackTrace = null) : base(code, message)
    {
        StackTrace = stackTrace;
    }
    public ErrorResponse(string? code, Exception? exception = null) : base(code, exception?.Message)
    {
#if DEBUG
        StackTrace = exception?.StackTrace;
#endif
    }
    public string? StackTrace { get; set; }
}

public static class ExceptionExtension
{
    public static string GetInnerStackTrace(this Exception? exception)
    {
        var sb = new StringBuilder();
        int level = 0;
        while (exception?.StackTrace != null)
        {
            sb = sb.Append("Level: ").Append(level++)
                .Append(", Exception: ")
                .Append(exception.Message)
                .Append(", StatckTrace: ")
                .Append(exception.StackTrace)
                .Append(Environment.NewLine);
            exception = exception.InnerException;
        }
        return sb.ToString();
    }
}
