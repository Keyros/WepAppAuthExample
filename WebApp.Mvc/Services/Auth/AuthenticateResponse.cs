namespace WebApp.Mvc.Services.Auth;

public class AuthenticateResponse
{
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
}

public class AuthenticateRequest
{
    public string? Login { get; set; }
    public string? Password { get; set; }
}