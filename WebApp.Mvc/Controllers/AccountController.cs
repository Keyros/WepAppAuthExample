using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Mvc.Models;
using WebApp.Mvc.Services.Interfaces;

namespace WebApp.Mvc.Controllers;

public class AccountController : Controller
{
    private readonly IAuthService _authService;

    public AccountController(IAuthService authService)
    {
        _authService = authService;
    }


    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login()
    {
        if (_authService.IsAuthenticated)
        {
            return RedirectToAction("Index", "Home");
        }

        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginModel loginModel)
    {
        if (await _authService.Login(loginModel.Login, loginModel.Password))
        {
            return RedirectToAction("Index", "Home");
        }

        return View();
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register() => View();

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _authService.Logout();
        return RedirectToAction("Login");
    }

    [AllowAnonymous]
    public IActionResult AccessDenied() => View();

    [HttpGet]
    [Authorize(Policy = "EvaluatedUsers", Roles = "Admin,Manager")]
    public IActionResult Test()
    {
        return Ok();
    }
}