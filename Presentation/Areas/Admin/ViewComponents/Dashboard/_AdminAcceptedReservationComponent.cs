using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.ViewComponents.Dashboard
{
    public class _AdminAcceptedReservationComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
