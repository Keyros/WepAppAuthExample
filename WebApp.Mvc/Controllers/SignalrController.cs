using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Mvc.Controllers;

[Authorize]
public class SignalrController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}