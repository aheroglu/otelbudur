using Microsoft.AspNetCore.Mvc;

namespace Presentation.ViewComponents.MainLayout
{
    public class _ScriptsComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
