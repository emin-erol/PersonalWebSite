using Microsoft.AspNetCore.Mvc;

namespace PersonalWebSite.ViewComponents.DefaultViewComponents
{
    public class _ContactMailComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
