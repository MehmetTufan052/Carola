using Carola.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Carola.WebUI.ViewComponents.HomePage
{
    public class _HomePageBrandComponentPartial : ViewComponent
    {
        private readonly IBrandService _brandService;

        public _HomePageBrandComponentPartial(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _brandService.GetAllBrandAsync();
            values ??= new();

            return View("~/Views/Shared/Components/HomePage/_HomePageBrandComponentPartial/Default.cshtml", values);
        }
    }
}
