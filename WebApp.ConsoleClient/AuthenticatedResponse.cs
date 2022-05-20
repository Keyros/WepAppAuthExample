using System.Text.Json.Serialization;

namespace WebApp.ConsoleClient;

public class AuthenticatedResponse
{
    [JsonPropertyName("token")]
    public string? Token { get; set; }
    [JsonPropertyName("refreshToken")]
    public string? RefreshToken { get; set; }
}

public class RefreshTokenRequest
{
    [JsonPropertyName("token")]
    public string? Token { get; set; }
    [JsonPropertyName("refreshToken")]
    public string? RefreshToken { get; set; }
}
