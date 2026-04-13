using Carola.BusinessLayer.Abstract;
using Carola.DtoLayer.Dtos.CarDtos;
using Carola.EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Carola.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CarController : Controller
    {
        private readonly ICarService _carService;
        private readonly ICategoryService _categoryService;

        public CarController(ICarService carService, ICategoryService categoryService)
        {
            _carService = carService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> CarList()
        {
            var values = await _carService.GetAllCarsWithCategoryAsync();
            return View(values);
        }
        public async Task<IActionResult> CreateCar()
        {
            ViewBag.Categories = new SelectList(await _categoryService.GetAllCategoryAsync(), "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCar(CreateCarDto createCarDto)
        {
            
                await _carService.CreateCarAsync(createCarDto);
                return RedirectToAction("CarList");
            
            
        }
    }
}
