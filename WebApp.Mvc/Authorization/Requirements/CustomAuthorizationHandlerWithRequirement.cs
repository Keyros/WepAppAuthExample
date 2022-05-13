using Microsoft.AspNetCore.Authorization;

namespace WebApp.Mvc.Authorization.Requirements;

public class CustomAuthorizationHandlerWithRequirement : AuthorizationHandler<CustomAuthorizationRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        CustomAuthorizationRequirement requirement)
    {
        return Task.CompletedTask;
    }
}