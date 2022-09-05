using ApplicationServices.Models.Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Backend.Api.Extensions;

public class ErrorWrappingMiddleware
{
    /*
     * Sử dụng để bắt lỗi global
     */
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorWrappingMiddleware> _logger;

    public ErrorWrappingMiddleware(RequestDelegate next, ILogger<ErrorWrappingMiddleware> logger)
    {
        _next = next;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            context.Response.StatusCode = 500;
        }

        if (!context.Response.HasStarted && context.Response.StatusCode != 204 && context.Response.StatusCode != 200)
        {
            context.Response.ContentType = "application/json";

            var response = new ApiErrorResult<bool>("Unknown Error");
            if (context.Response.StatusCode == 401)
            {
                response.Message = "Unauthorized";
            }

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}