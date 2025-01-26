using Microsoft.AspNetCore.Mvc;

namespace PersonalWebSite.ViewComponents.ManagementUILayoutViewComponents
{
    public class _ManagementHeadUILayoutComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
