using Microsoft.AspNetCore.Mvc;

namespace Carola.WebUI.ViewComponents.HomePage
{
    public class _HomePageFeaturedCarsComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/Views/Shared/Components/HomePage/_HomePageFeaturedCarsComponentPartial/Default.cshtml");
        }
    }
}
