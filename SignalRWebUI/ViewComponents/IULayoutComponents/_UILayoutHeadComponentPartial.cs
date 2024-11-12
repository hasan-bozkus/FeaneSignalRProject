using Microsoft.AspNetCore.Mvc;

namespace SignalRWebUI.ViewComponents.IULayoutComponents
{
    public class _UILayoutHeadComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
