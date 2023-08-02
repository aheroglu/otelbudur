using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Staff.ViewComponents.StaffLayout
{
    public class _StaffSidebarComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
