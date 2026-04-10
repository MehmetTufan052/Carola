using Microsoft.AspNetCore.Mvc;

namespace Carola.WebUI.ViewComponents.ClientLayout
{
    public class _ClientLayoutSearchPopupComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/Views/Shared/Components/ClientLayout/_ClientLayoutSearchPopupComponentPartial/Default.cshtml");
        }
    }
}
