using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.ViewComponents.Dashboard
{
    public class _AdminCompletedReservationComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
