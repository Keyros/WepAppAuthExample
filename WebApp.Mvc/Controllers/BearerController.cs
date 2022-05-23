using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Mvc.Services.Auth;

namespace WebApp.Mvc.Controllers;

public class BearerController : Controller
{
    private readonly IAuthService _authService;

    public BearerController(IAuthService authService)
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost("[controller]/token")]
    public async Task<IActionResult> Token([FromBody]AuthenticateRequest request)
    {
        var tokenData = await _authService.GetToken(request);
        if (tokenData == null)
        {
            return Unauthorized();
        }

        return Json(tokenData);
    }

    [HttpPost("[controller]/refresh")]
    [AllowAnonymous]
    public async Task<IActionResult> RefreshToken([FromBody]RefreshTokenRequest request)
    {
        //проверить их на валидность AT
        //достать информацию о пользователе
        //проверить refreshToken который пришел с тем что в бд
        //посмотреть он еще работает или нет
        //сгенерить новую пару токенов 
        //обновить accountInfo
        //вернуть новые токены
        //если все плохо return Unauthorized();
        var tokenData = await _authService.RefreshTokens(request);
        if (tokenData == null)
        {
            return Unauthorized();
        }
        return Json(tokenData);
    }


    [Authorize]
    public async Task<IActionResult> LogOut()
    {
        await _authService.Logout();
        return Ok();
    }
}