using Microsoft.AspNetCore.Mvc;

namespace SignalRWebUI.ViewComponents.IULayoutComponents
{
    public class _UILayoutNavbarComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
