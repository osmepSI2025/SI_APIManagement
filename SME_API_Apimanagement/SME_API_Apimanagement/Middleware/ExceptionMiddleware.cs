using Microsoft.AspNetCore.Http;
using SME_API_Apimanagement.Entities;
using SME_API_Apimanagement.Models;
using SME_API_Apimanagement.Repository;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceScopeFactory _scopeFactory;  

    public ExceptionMiddleware(RequestDelegate next, IServiceScopeFactory scopeFactory)
    {
        _next = next;
        _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            var errorLog = new TErrorApiLog();
            await _next(httpContext);

            // ... inside your InvokeAsync method

            if (httpContext.Response.StatusCode != 200)
            {
                switch (httpContext.Response.StatusCode)
                {
                    case (int)HttpStatusCode.BadRequest: // 400
                        errorLog.HttpCode = httpContext.Response.StatusCode.ToString();
                        errorLog.Message = "Bad request";
                        errorLog.StackTrace = "The request could not be understood or was missing required parameters.";
                        errorLog.ErrorDate = DateTime.Now;
                        errorLog.Path = httpContext.Request.Path;
                        errorLog.HttpMethod = httpContext.Request.Method;
                        errorLog.CreatedBy = httpContext.User.Identity?.Name ?? "system";
                        break;
                    case (int)HttpStatusCode.Unauthorized: // 401
                        errorLog.HttpCode = httpContext.Response.StatusCode.ToString();
                        errorLog.Message = "Unauthorized access";
                        errorLog.StackTrace = "The request requires user authentication.";
                        errorLog.ErrorDate = DateTime.Now;
                        errorLog.Path = httpContext.Request.Path;
                        errorLog.HttpMethod = httpContext.Request.Method;
                        errorLog.CreatedBy = httpContext.User.Identity?.Name ?? "system";
                        break;
                    case (int)HttpStatusCode.Forbidden: // 403
                        errorLog.HttpCode = httpContext.Response.StatusCode.ToString();
                        errorLog.Message = "Forbidden";
                        errorLog.StackTrace = "The server understood the request, but refuses to authorize it.";
                        errorLog.ErrorDate = DateTime.Now;
                        errorLog.Path = httpContext.Request.Path;
                        errorLog.HttpMethod = httpContext.Request.Method;
                        errorLog.CreatedBy = httpContext.User.Identity?.Name ?? "system";
                        break;
                    case (int)HttpStatusCode.NotFound: // 404
                        errorLog.HttpCode = httpContext.Response.StatusCode.ToString();
                        errorLog.Message = "Resource not found";
                        errorLog.StackTrace = "The requested resource could not be found.";
                        errorLog.ErrorDate = DateTime.Now;
                        errorLog.Path = httpContext.Request.Path;
                        errorLog.HttpMethod = httpContext.Request.Method;
                        errorLog.CreatedBy = httpContext.User.Identity?.Name ?? "system";
                        break;
                    case (int)HttpStatusCode.MethodNotAllowed: // 405
                        errorLog.HttpCode = httpContext.Response.StatusCode.ToString();
                        errorLog.Message = "Method not allowed";
                        errorLog.StackTrace = "The HTTP method is not allowed for the requested resource.";
                        errorLog.ErrorDate = DateTime.Now;
                        errorLog.Path = httpContext.Request.Path;
                        errorLog.HttpMethod = httpContext.Request.Method;
                        errorLog.CreatedBy = httpContext.User.Identity?.Name ?? "system";
                        break;
                    case (int)HttpStatusCode.InternalServerError: // 500
                        errorLog.HttpCode = httpContext.Response.StatusCode.ToString();
                        errorLog.Message = "Internal server error";
                        errorLog.StackTrace = "An unexpected error occurred on the server.";
                        errorLog.ErrorDate = DateTime.Now;
                        errorLog.Path = httpContext.Request.Path;
                        errorLog.HttpMethod = httpContext.Request.Method;
                        errorLog.CreatedBy = httpContext.User.Identity?.Name ?? "system";
                        break;
                }

                errorLog.InnerException = null;
                errorLog.Source = "ExceptionMiddleware";
                errorLog.TargetSite = "InvokeAsync";
                errorLog.SystemCode = "SYS-API";
                errorLog.CreatedBy = "system";

                using var scope = _scopeFactory.CreateScope();
                var _errorApiLogRepository = scope.ServiceProvider.GetRequiredService<ITErrorApiLogRepository>();
                await _errorApiLogRepository.AddAsync(errorLog);

                var statusCode = httpContext.Response.StatusCode;
                var isError = statusCode == (int)HttpStatusCode.BadRequest ||
                              statusCode == (int)HttpStatusCode.Unauthorized ||
                              statusCode == (int)HttpStatusCode.Forbidden ||
                              statusCode == (int)HttpStatusCode.NotFound ||
                              statusCode == (int)HttpStatusCode.MethodNotAllowed ||
                              statusCode == (int)HttpStatusCode.InternalServerError;

                if (isError)
                {
                    httpContext.Response.ContentType = "application/json";
                    var errorResponse = new ErrorResponseModels
                    {
                        responseCode = statusCode.ToString(),
                        responseMsg = errorLog.Message
                    };
                    var json = JsonSerializer.Serialize(errorResponse);
                    await httpContext.Response.WriteAsync(json);
                }
            }


        }
        catch (Exception ex)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await httpContext.Response.WriteAsync("{}");
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError; // 500 if unexpected

        // You can add more specific exception types here if needed
        if (exception is ArgumentException)
            code = HttpStatusCode.BadRequest;

        var result = JsonSerializer.Serialize(new
        {
            error = exception.Message,
            detail = exception.InnerException?.Message
        });

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(result);
    }
}