using Microsoft.AspNetCore.Mvc;

namespace EFCoreQueryFilters.Controllers;

public class EventController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}