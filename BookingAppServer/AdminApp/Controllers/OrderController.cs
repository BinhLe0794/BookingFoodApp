using Microsoft.AspNetCore.Mvc;

namespace AdminApp.Controllers;

public class OrderController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}