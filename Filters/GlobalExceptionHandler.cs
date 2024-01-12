using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;

namespace dbhealthcare.Filters;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;

        var (status, title) = MapExeception(exception);

        await Results.Problem(
            type:"Bad Request",
            statusCode : status,
            title : title,
            extensions : new Dictionary<string, object?>
            {
                {"traceId",traceId }
            }).ExecuteAsync(httpContext);

        return true;
    }

    private static (int status, string title) MapExeception(Exception exception)
    {
        return exception switch
        {
            ArgumentNullException => (StatusCodes.Status400BadRequest, exception.Message),
            IndexOutOfRangeException => (StatusCodes.Status400BadRequest, exception.Message),
            _ => (StatusCodes.Status500InternalServerError, "Working on it")
        };

    }
}
