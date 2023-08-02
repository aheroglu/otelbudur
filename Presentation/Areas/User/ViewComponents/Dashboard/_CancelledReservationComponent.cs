using DataAccess.Concrete;
using Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Areas.User.ViewComponents.Dashboard
{
    public class _CancelledReservationComponent : ViewComponent
    {
        private readonly Context _context;
        private readonly UserManager<AppUser> _userManager;

        public _CancelledReservationComponent(Context context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.CancelledReservationCount = _context.Bookings.Count(x => x.UserId == user.Id && x.ReservationStatus == "Cancelled");
            return View();
        }
    }
}
