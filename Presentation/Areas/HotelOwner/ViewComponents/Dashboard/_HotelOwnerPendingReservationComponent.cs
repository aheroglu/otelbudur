using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.HotelOwner.ViewComponents.Dashboard
{
    public class _HotelOwnerPendingReservationComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
