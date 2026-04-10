using Microsoft.AspNetCore.Mvc;

namespace Carola.WebUI.ViewComponents.ClientLayout
{
    public class _ClientLayoutHeaderComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/Views/Shared/Components/ClientLayout/_ClientLayoutHeaderComponentPartial/Default.cshtml");
        }
    }
}
