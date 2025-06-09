using Microsoft.AspNetCore.Mvc;

namespace PersonalWebSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/UILayout")]
    public class UILayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
