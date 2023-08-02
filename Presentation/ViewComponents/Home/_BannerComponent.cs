using Microsoft.AspNetCore.Mvc;

namespace Presentation.ViewComponents.MainLayout
{
    public class _BannerComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
