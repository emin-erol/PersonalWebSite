using Microsoft.AspNetCore.Mvc;

namespace PersonalWebSite.Areas.Admin.ViewComponents.UILayoutViewComponents
{
    [ViewComponent(Name = "AdminNavbarUILayout")]
    public class _NavbarUILayoutComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
