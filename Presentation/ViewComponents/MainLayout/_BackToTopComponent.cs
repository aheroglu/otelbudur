using Microsoft.AspNetCore.Mvc;

namespace Presentation.ViewComponents.MainLayout
{
    public class _BackToTopComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
