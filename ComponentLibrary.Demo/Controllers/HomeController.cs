using Microsoft.AspNetCore.Mvc;

namespace ComponentLibrary.Demo.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}