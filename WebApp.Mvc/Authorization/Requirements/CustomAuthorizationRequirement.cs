using Microsoft.AspNetCore.Authorization;

namespace WebApp.Mvc.Authorization.Requirements;

public class CustomAuthorizationRequirement : IAuthorizationRequirement
{
    public CustomAuthorizationRequirement(string data)
    {
        Data = data;  
    }
    public string Data { get; set; }
}