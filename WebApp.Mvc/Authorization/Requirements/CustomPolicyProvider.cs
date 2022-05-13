using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace WebApp.Mvc.Authorization.Requirements;

public class CustomPolicyProvider : IAuthorizationPolicyProvider
{
    private DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; }

    public CustomPolicyProvider(IOptions<AuthorizationOptions> options)
    {
        FallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
    }


    public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        if (policyName == "TestPolicy")
        {
            var policy = new AuthorizationPolicyBuilder();
            policy.AddRequirements(new CustomAuthorizationRequirement("TestPolicy"));
            return Task.FromResult(policy.Build())!;
        }

        return FallbackPolicyProvider.GetPolicyAsync(policyName);
    }

    public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        => FallbackPolicyProvider.GetDefaultPolicyAsync();


    public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
        => FallbackPolicyProvider.GetFallbackPolicyAsync();
}