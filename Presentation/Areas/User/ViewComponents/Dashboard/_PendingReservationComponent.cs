using DataAccess.Concrete;
using Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Areas.User.ViewComponents.Dashboard
{
    public class _PendingReservationComponent : ViewComponent
    {
        private readonly Context _context;
        private readonly UserManager<AppUser> _userManager;

        public _PendingReservationComponent(UserManager<AppUser> userManager, Context context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.PendingReservationCount = _context.Bookings.Count(x => x.UserId == user.Id && x.ReservationStatus == "Pending");
            return View();
        }
    }
}
