using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.HotelOwner.ViewComponents.HotelOwnerLayout
{
    public class _HotelOwnerSidebarComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
