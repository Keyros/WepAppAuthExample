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
    public async Task<IActionResult> Token(string username, string password)
    {
        var tokenData = await _authService.GetToken(username, password);
        if (tokenData == null)
        {
            return Unauthorized();
        }

        return Json(tokenData);
    }

    [HttpPost("[controller]/refresh")]
    [AllowAnonymous]
    public async Task<IActionResult> RefreshToken()
    {
        throw new NotImplementedException();
    }


    [Authorize]
    public async Task<IActionResult> LogOut()
    {
        await _authService.Logout();
        return Ok();
    }
}