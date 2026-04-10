using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Carola.WebUI.ViewComponents.ClientLayout
{
    public class _ClientLayoutHeadComponentPartial: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
             return View("~/Views/Shared/Components/ClientLayout/_ClientLayoutHeadComponentPartial/Default.cshtml");
        }
    }
}
