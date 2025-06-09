using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PersonalWebSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/MainPage")]
    public class MainPageController : Controller
    {
        [Route("Index")]
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
