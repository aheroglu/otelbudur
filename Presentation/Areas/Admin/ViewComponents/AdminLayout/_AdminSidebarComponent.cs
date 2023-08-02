using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.ViewComponents.AdminLayout
{
    public class _AdminSidebarComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
