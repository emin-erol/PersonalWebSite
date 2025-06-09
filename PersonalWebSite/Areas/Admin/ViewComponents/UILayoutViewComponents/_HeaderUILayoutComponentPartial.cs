using Microsoft.AspNetCore.Mvc;

namespace PersonalWebSite.Areas.Admin.ViewComponents.UILayoutViewComponents
{
    [ViewComponent(Name = "AdminHeaderUILayout")]
    public class _HeaderUILayoutComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
