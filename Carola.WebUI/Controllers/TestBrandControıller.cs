using Carola.BusinessLayer.Abstract;
using Carola.DtoLayer.Dtos.BrandDtos;
using Carola.EntityLayer.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Carola.WebUI.Controllers
{
    public class TestBrandControıller : Controller
    {
          private readonly IBrandService _brandService;

        public TestBrandControıller(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public IActionResult CreateBrand()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand(CreateBrandDto createBrandDto)
        {
            if (!ModelState.IsValid)
                return View(createBrandDto); // Hataları view'a geri döndür

            try
            {
                await _brandService.CreateBrandAsync(createBrandDto);
                return RedirectToAction("Index");
            }
            catch (ValidationException ex)
            {
                foreach (var error in ex.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                return View(createBrandDto);
            }  
        }
    }
}
