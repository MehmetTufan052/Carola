using Carola.BusinessLayer.Abstract;
using Carola.DtoLayer.Dtos.BrandDtos;
using Carola.EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Carola.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public async Task<IActionResult> BrandList()
        {
            var values=await _brandService.GetAllBrandAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateBrand()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateBrand(CreateBrandDto createBrandDto)
        {
            await _brandService.CreateBrandAsync(createBrandDto);
            return RedirectToAction("BrandList");
        }
        
        [HttpGet]
        public async Task<IActionResult> UpdateBrand(int id)
        {
            var value= await _brandService.GetBrandByIdAsync(id);
            return View(value);
        }
        
        [HttpPost]
        public async Task<IActionResult> UpdateBrand(UpdateBrandDto updateBrandDto)
        {
            await _brandService.UpdateBrandAsync(updateBrandDto);
            return RedirectToAction("BrandList");
        }

        public async Task<IActionResult> DeleteBrand(int id)
        {  
            await _brandService.DeleteBrandAsync(id);
            return RedirectToAction("BrandList");
        }
    }
}
