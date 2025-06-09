using Microsoft.AspNetCore.Mvc;

namespace PersonalWebSite.Areas.Admin.ViewComponents.UILayoutViewComponents
{
    [ViewComponent(Name = "AdminFooterUILayout")]
    public class _FooterUILayoutComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
