using Carola.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Carola.WebUI.ViewComponents.HomePage
{
    public class _HomePageCarTypeComponentPartial : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public _HomePageCarTypeComponentPartial(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _categoryService.GetAllCategoryAsync();
            values ??= new();

            return View("~/Views/Shared/Components/HomePage/_HomePageCarTypeComponentPartial/Default.cshtml", values);
        }
    }
}
