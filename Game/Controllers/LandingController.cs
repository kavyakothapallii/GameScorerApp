using Microsoft.AspNetCore.Mvc;

namespace Game.Controllers
{
    public class LandingController : Controller
    {
        public IActionResult Landing()
        {
            return View();
        }
    }
}
