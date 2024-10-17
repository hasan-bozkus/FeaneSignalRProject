using Microsoft.AspNetCore.Mvc;

namespace SignalRWebUI.Controllers
{
    public class AdminLayoutController : Controller
    {
        public IActionResult _AdminLayout()
        {
            return View();
        }
    }
}
