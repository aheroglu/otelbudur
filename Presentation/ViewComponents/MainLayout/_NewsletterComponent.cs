using Microsoft.AspNetCore.Mvc;

namespace Presentation.ViewComponents.MainLayout
{
    public class _NewsletterComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
