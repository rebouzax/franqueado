using Franqueado.Application.Abstractions;

namespace Franqueado.Api.DependencyInjection;

public sealed class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _accessor;

    public CurrentUser(IHttpContextAccessor accessor) => _accessor = accessor;

    public string? Username
        => _accessor.HttpContext?.Request.Headers["X-User"].FirstOrDefault();
}
