using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace WebApp.Mvc.Authorization.Bearer;

public class AuthOptions
{
    public const string ISSUER = "webAppServer"; // издатель токена
    public const string AUDIENCE = "webAppClient"; // потребитель токена
    const string KEY = "this is my custom Secret key for authentication"; // ключ для шифрации
    public static TimeSpan LIFETIME = TimeSpan.FromSeconds(5);
    public static SymmetricSecurityKey GetSymmetricSecurityKey() => new(Encoding.UTF8.GetBytes(KEY));
}