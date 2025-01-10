using Microsoft.AspNetCore.Mvc;

namespace PersonalWebSite.Controllers
{
    public class UILayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
