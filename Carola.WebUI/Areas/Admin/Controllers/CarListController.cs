using Carola.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Carola.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CarListController : Controller
    {
        private readonly ICarService _carService;

        public CarListController(ICarService carService)
        {
            _carService = carService;
        }

        public async Task<IActionResult> CarList()
        {
            var values = await _carService.GetAllCarsWithCategoryAsync();
            return View("CarListPage", values);
        }

        public async Task<IActionResult> CarListPage()
        {
            var values = await _carService.GetAllCarsWithCategoryAsync();
            return View(values);
        }
    }
}
