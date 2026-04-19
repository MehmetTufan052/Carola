using Microsoft.AspNetCore.Mvc;

namespace Carola.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminLayoutController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
        }
    }
}
