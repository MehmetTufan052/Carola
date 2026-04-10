using Microsoft.AspNetCore.Mvc;

namespace Carola.WebUI.ViewComponents.ClientLayout
{
    public class _ClientLayoutFooterComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/Views/Shared/Components/ClientLayout/_ClientLayoutFooterComponentPartial/Default.cshtml");
        }
    }
}
