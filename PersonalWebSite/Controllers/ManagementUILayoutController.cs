using Microsoft.AspNetCore.Mvc;

namespace PersonalWebSite.Controllers
{
    public class ManagementUILayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
