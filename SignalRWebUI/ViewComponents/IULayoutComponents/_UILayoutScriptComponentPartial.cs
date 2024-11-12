using Microsoft.AspNetCore.Mvc;

namespace SignalRWebUI.ViewComponents.IULayoutComponents
{
    public class _UILayoutScriptComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
