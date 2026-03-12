using Microsoft.AspNetCore.Mvc;

namespace Carola.WebUI.Controllers
{
    public class CarController : Controller
    {
        public IActionResult CarList()
        {
            return View();
        }
        public IActionResult CreateCar()
        {
            return View();
        }

    }
}
