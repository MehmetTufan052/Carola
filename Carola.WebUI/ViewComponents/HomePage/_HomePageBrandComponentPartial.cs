using Microsoft.AspNetCore.Mvc;

namespace Carola.WebUI.ViewComponents.HomePage
{
    public class _HomePageBrandComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/Views/Shared/Components/HomePage/_HomePageBrandComponentPartial/Default.cshtml");
        }
    }
}
