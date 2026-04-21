using Carola.BusinessLayer.Abstract;
using Carola.DtoLayer.Dtos.CarDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Carola.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Car")]
    public class CarController : Controller
    {
        private readonly ICarService _carService;
        private readonly ICategoryService _categoryService;

        public CarController(ICarService carService, ICategoryService categoryService)
        {
            _carService = carService;
            _categoryService = categoryService;
        }

        [HttpGet("CarList")]
        public async Task<IActionResult> CarList()
        {
            var values = await _carService.GetAllCarsWithCategoryAsync();
            Response.Headers["X-Carola-View"] = "Admin/Car/CarList";
            return View(values);
        }

        [HttpGet("CreateCar")]
        public async Task<IActionResult> CreateCar()
        {
            ViewBag.Categories = new SelectList(await _categoryService.GetAllCategoryAsync(), "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        [Route("CreateCar")]
        public async Task<IActionResult> CreateCar(CreateCarDto createCarDto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(await _categoryService.GetAllCategoryAsync(), "CategoryId", "CategoryName", createCarDto.CategoryId);
                return View(createCarDto);
            }

            await _carService.CreateCarAsync(createCarDto);
            return RedirectToAction("CarList");
        }

        [HttpGet]
        [Route("UpdateCar/{id:int}")]
        public async Task<IActionResult> UpdateCar(int id)
        {
            var car = await _carService.GetCarByIdAsync(id);
            if (car == null)
            {
                return RedirectToAction("CarList");
            }

            var updateCarDto = new UpdateCarDto
            {
                CarId = car.CarId,
                Brand = car.Brand,
                Model = car.Model,
                ModelYear = car.ModelYear,
                PlateNumber = car.PlateNumber,
                DailyPrice = car.DailyPrice,
                SeatCount = car.SeatCount,
                LuggageCapacity = car.LuggageCapacity,
                Mileage = car.Mileage,
                IsAvailable = car.IsAvailable,
                FuelType = car.FuelType,
                TransmissionType = car.TransmissionType,
                CategoryId = car.CategoryId,
                Category = car.Category,
                ImageUrl = car.ImageUrl,
                Status = car.Status
            };

            ViewBag.Categories = new SelectList(
                await _categoryService.GetAllCategoryAsync(),
                "CategoryId",
                "CategoryName",
                updateCarDto.CategoryId);

            return View(updateCarDto);
        }

        [HttpPost]
        [Route("UpdateCar/{id:int?}")]
        public async Task<IActionResult> UpdateCar(UpdateCarDto updateCarDto)
        {
            ModelState.Remove(nameof(updateCarDto.Category));

            if (updateCarDto.CarId <= 0)
            {
                ModelState.AddModelError(nameof(updateCarDto.CarId), "Geçerli bir araç kaydı bulunamadı.");
            }

            if (string.IsNullOrWhiteSpace(updateCarDto.Brand))
            {
                ModelState.AddModelError(nameof(updateCarDto.Brand), "Marka alanı zorunludur.");
            }

            if (string.IsNullOrWhiteSpace(updateCarDto.Model))
            {
                ModelState.AddModelError(nameof(updateCarDto.Model), "Model alanı zorunludur.");
            }

            if (string.IsNullOrWhiteSpace(updateCarDto.PlateNumber))
            {
                ModelState.AddModelError(nameof(updateCarDto.PlateNumber), "Plaka alanı zorunludur.");
            }

            if (string.IsNullOrWhiteSpace(updateCarDto.FuelType))
            {
                ModelState.AddModelError(nameof(updateCarDto.FuelType), "Yakıt tipi zorunludur.");
            }

            if (string.IsNullOrWhiteSpace(updateCarDto.TransmissionType))
            {
                ModelState.AddModelError(nameof(updateCarDto.TransmissionType), "Vites tipi zorunludur.");
            }

            if (updateCarDto.CategoryId <= 0)
            {
                ModelState.AddModelError(nameof(updateCarDto.CategoryId), "Lütfen geçerli bir kategori seçin.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(
                    await _categoryService.GetAllCategoryAsync(),
                    "CategoryId",
                    "CategoryName",
                    updateCarDto.CategoryId);

                return View(updateCarDto);
            }

            await _carService.UpdateCarAsync(updateCarDto);
            return RedirectToAction("CarList");
        }

        [HttpPost]
        [Route("DeleteCar/{id:int}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            await _carService.DeleteCarAsync(id);
            return RedirectToAction("CarList");
        }
    }
}
