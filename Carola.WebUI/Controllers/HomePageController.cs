using Microsoft.AspNetCore.Mvc;

namespace Carola.WebUI.Controllers
{
    public class HomePageController : Controller
    {
        public IActionResult HomePage()
        {
            return View();
        }
    }
}
