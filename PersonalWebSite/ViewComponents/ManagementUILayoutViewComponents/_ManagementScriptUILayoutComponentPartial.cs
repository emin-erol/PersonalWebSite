using Microsoft.AspNetCore.Mvc;

namespace PersonalWebSite.ViewComponents.ManagementUILayoutViewComponents
{
    public class _ManagementScriptUILayoutComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
