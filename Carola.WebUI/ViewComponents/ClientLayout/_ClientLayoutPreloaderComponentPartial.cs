using Microsoft.AspNetCore.Mvc;

namespace Carola.WebUI.ViewComponents.ClientLayout
{
    public class _ClientLayoutPreloaderComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/Views/Shared/Components/ClientLayout/_ClientLayoutPreloaderComponentPartial/Default.cshtml");
        }
    }
}
