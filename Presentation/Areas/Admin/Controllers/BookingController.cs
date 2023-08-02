using Business.Concrete;
using DataAccess.EntityFramework;
using Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BookingController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        BookingManager bookingManager = new BookingManager(new EfBookingDal());
        RoomManager roomManager = new RoomManager(new EfRoomDal());

        public BookingController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index(int page = 1)
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }

            var values = bookingManager.TBookingList().ToPagedList(page, 10);

            return View(values);
        }
    }
}
