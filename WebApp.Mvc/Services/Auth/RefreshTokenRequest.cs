namespace WebApp.Mvc.Services.Auth;

public class RefreshTokenRequest
{
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
}