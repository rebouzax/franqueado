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
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";

            var errors = ex.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(x => x.ErrorMessage).ToArray());

            var payload = new
            {
                title = "Validation error",
                status = 400,
                errors
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(payload));
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var payload = new
            {
                title = "Server error",
                status = 500,
                detail = context.RequestServices
                    .GetRequiredService<IHostEnvironment>()
                    .IsDevelopment()
                        ? ex.ToString()
                        : "Unexpected error."
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(payload));
        }
    }
}
