using Microsoft.AspNetCore.Mvc;

namespace Carola.WebUI.ViewComponents.HomePage
{
    public class _HomePageBrand2ComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/Views/Shared/Components/HomePage/_HomePageBrand2ComponentPartial/Default.cshtml");
        }
    }
}
