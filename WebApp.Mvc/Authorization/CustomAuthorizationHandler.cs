using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using WebApp.Dal;

namespace WebApp.Mvc.Authorization;

public class CustomAuthorizationHandler : IAuthorizationHandler
{
    private readonly IDbContextFactory<WebAppDbContext> _factory;
    private readonly ILogger<CustomAuthorizationHandler> _logger;

    public CustomAuthorizationHandler(IDbContextFactory<WebAppDbContext> factory, ILogger<CustomAuthorizationHandler> logger)
    {
        _factory = factory;
        _logger = logger;
    }

    public Task HandleAsync(AuthorizationHandlerContext context)
    {
        _logger.LogInformation("CustomAuthorizationHandler");
        _logger.LogInformation($"User: {context.User.Identity?.Name}");
        var httpContext = context.Resource as DefaultHttpContext;
      
        var controller = httpContext?.Request.Path ?? string.Empty;
        _logger.LogInformation($"Resource: {controller}");
        return Task.CompletedTask;
    }
}
