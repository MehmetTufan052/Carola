using Carola.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Carola.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/CarList")]
    public class CarListController : Controller
    {
        private readonly ICarService _carService;

        public CarListController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet("CarList")]
        public async Task<IActionResult> CarList()
        {
            var values = await _carService.GetAllCarsWithCategoryAsync();
            Response.Headers["X-Carola-View"] = "Admin/CarList/CarList";
            return View(values);
        }

        [HttpGet("CarListPage")]
        public async Task<IActionResult> CarListPage()
        {
            var values = await _carService.GetAllCarsWithCategoryAsync();
            Response.Headers["X-Carola-View"] = "Admin/CarList/CarListPage";
            return View(values);
        }
    }
}
