using Carola.BusinessLayer.Abstract;
using Carola.DataAccessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Carola.WebUI.ViewComponents.HomePage
{
    public class _HomePageServiceComponentPartial:ViewComponent
    {
        private readonly ILocationService _locationService;

        public _HomePageServiceComponentPartial(ILocationService locationService)
        {
            _locationService = locationService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _locationService.GetAllLocationAsync();
            return View("~/Views/Shared/Components/HomePage/_HomePageServiceComponentPartial/Default.cshtml",values);
        }
    }
}
