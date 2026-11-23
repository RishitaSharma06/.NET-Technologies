using Microsoft.AspNetCore.Mvc;

namespace StudentCourseRegistration.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}

