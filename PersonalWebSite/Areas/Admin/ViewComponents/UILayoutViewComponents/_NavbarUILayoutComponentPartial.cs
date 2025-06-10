using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PersonalWebSite.Areas.Admin.ViewComponents.UILayoutViewComponents
{
    [ViewComponent(Name = "AdminNavbarUILayout")]
    public class _NavbarUILayoutComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            ViewBag.UserName = UserClaimsPrincipal?.FindFirst(ClaimTypes.Name)?.Value;

            return View();
        }
    }
}
