using Carola.BusinessLayer.Abstract;
using Carola.DtoLayer.Dtos.CarDtos;
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

        [HttpGet]
        public async Task<IActionResult> UpdateCar(int id)
        {
            var car = await _carService.GetCarByIdAsync(id);
            if (car == null)
            {
                return RedirectToAction("CarList");
            }

            ViewBag.Categories = new SelectList(
                await _categoryService.GetAllCategoryAsync(),
                "CategoryId",
                "CategoryName",
                car.CategoryId);

            return View(car);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCar(UpdateCarDto updateCarDto)
        {
            await _carService.UpdateCarAsync(updateCarDto);
            return RedirectToAction("CarList");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCar(int id)
        {
            await _carService.DeleteCarAsync(id);
            return RedirectToAction("CarList");
        }
    }
}
