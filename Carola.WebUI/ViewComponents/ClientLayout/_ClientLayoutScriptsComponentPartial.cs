using Microsoft.AspNetCore.Mvc;

namespace Carola.WebUI.ViewComponents.ClientLayout
{
    public class _ClientLayoutScriptsComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/Views/Shared/Components/ClientLayout/_ClientLayoutScriptsComponentPartial/Default.cshtml");
        }
    }
}
