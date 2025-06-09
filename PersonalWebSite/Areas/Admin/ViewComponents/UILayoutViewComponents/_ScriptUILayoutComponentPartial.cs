using Microsoft.AspNetCore.Mvc;

namespace PersonalWebSite.Areas.Admin.ViewComponents.UILayoutViewComponents
{
    [ViewComponent(Name = "AdminScriptUILayout")]
    public class _ScriptUILayoutComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
