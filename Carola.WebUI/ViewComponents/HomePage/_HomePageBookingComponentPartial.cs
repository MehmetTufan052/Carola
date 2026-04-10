using Microsoft.AspNetCore.Mvc;

namespace Carola.WebUI.ViewComponents.HomePage
{
    public class _HomePageBookingComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/Views/Shared/Components/HomePage/_HomePageBookingComponentPartial/Default.cshtml");
        }
    }
}
