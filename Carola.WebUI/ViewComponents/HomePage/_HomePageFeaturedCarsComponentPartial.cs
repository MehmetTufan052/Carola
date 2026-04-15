using Carola.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Carola.WebUI.ViewComponents.HomePage
{
    public class _HomePageFeaturedCarsComponentPartial : ViewComponent
    {
        private readonly ICarService _carService;

        public _HomePageFeaturedCarsComponentPartial(ICarService carService)
        {
            _carService = carService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _carService.GetAllCarsWithCategoryAsync();
            values ??= new();

            return View("~/Views/Shared/Components/HomePage/_HomePageFeaturedCarsComponentPartial/Default.cshtml", values);
        }
    }
}
