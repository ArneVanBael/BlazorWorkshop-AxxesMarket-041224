using AxxesMarket.Shared.WebModels;
using System.Net;
using System.Text.Json;

namespace AxxesMarket.Api.Middleware;

public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    public async Task InvokeAsync(HttpContext context, ILogger<GlobalExceptionHandlingMiddleware> logger)
    {
        try
        {
            // set user to current thread
            if(context.User is not null)
            {
                Thread.CurrentPrincipal = context.User;
            }

            await _next(context);
        }
        catch (Exception ex)
        {
            var exception = ex;
            if (ex is AggregateException)
            {
                exception = ex.InnerException;
            }

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            await TransformResponseAndLogException(context, logger, exception);
        }
    }

    private async Task TransformResponseAndLogException(HttpContext context, ILogger logger, Exception? exception)
    {
        var response = new JsonResponse { Errors = new List<string> { "Something went wrong" } };

        var result = JsonSerializer.Serialize(response);

        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json; charset=utf-8";
        await context.Response.WriteAsync(result);
        logger.LogError(exception, "Error occurred in application. Messages: {@errorMessages}", response.Errors);
    }
}