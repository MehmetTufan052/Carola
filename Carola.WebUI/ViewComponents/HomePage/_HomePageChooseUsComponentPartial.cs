using Microsoft.AspNetCore.Mvc;

namespace Carola.WebUI.ViewComponents.HomePage
{
    public class _HomePageChooseUsComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/Views/Shared/Components/HomePage/_HomePageChooseUsComponentPartial/Default.cshtml");
        }
    }
}
