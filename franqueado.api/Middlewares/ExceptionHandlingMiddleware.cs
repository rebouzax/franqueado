using FluentValidation;
using System.Net;
using System.Text.Json;

namespace Franqueado.Api.Middlewares;

public sealed class ExceptionHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException ex)
        {
            await WriteAsync(context, HttpStatusCode.BadRequest, new
            {
                title = "Validation error",
                status = 400,
                errors = ex.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(g => g.Key, g => g.Select(x => x.ErrorMessage).ToArray())
            });
        }
        catch (KeyNotFoundException ex)
        {
            await WriteAsync(context, HttpStatusCode.NotFound, new
            {
                title = "Not found",
                status = 404,
                detail = ex.Message
            });
        }
        catch (InvalidOperationException ex)
        {
            await WriteAsync(context, HttpStatusCode.Conflict, new
            {
                title = "Conflict",
                status = 409,
                detail = ex.Message
            });
        }
        catch (Exception ex)
        {
            var env = context.RequestServices.GetRequiredService<IHostEnvironment>();

            await WriteAsync(context, HttpStatusCode.InternalServerError, new
            {
                title = "Server error",
                status = 500,
                detail = env.IsDevelopment() ? ex.ToString() : "Unexpected error."
            });
        }
    }

    private static async Task WriteAsync(HttpContext context, HttpStatusCode statusCode, object payload)
    {
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(payload));
    }
}
