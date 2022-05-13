using Microsoft.AspNetCore.Mvc;
using WebApp.Mvc.Services.Interfaces;

namespace WebApp.Mvc.Controllers;

public class BearerController : Controller
{
    private readonly IAuthService _authService;

    public BearerController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("[controller]/token")]
    public async Task<IActionResult> Token(string username, string password)
    {
        var tokenData = await _authService.GetToken(username, password);
        return Json(new {tokenData.token, tokenData.name});
    }
}