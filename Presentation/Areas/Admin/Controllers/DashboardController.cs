using Business.Concrete;
using DataAccess.Concrete;
using DataAccess.EntityFramework;
using Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using X.PagedList;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly Context _context;

        BookingManager bookingManager = new BookingManager(new EfBookingDal());

        public DashboardController(Context context)
        {
            _context = context;
        }

        public IActionResult Index(int page = 1)
        {
            ViewBag.PendingReservationCount = _context.Bookings.Count(x => x.ReservationStatus == "Pending");
            ViewBag.AcceptedReservationCount = _context.Bookings.Count(x => x.ReservationStatus == "Accepted");
            ViewBag.CancelledReservationCount = _context.Bookings.Count(x => x.ReservationStatus == "Cancelled");
            ViewBag.CompletedReservationCount = _context.Bookings.Count(x => x.ReservationStatus == "Completed");

            var bookings = bookingManager.TBookingList().ToPagedList(page, 10);
            return View(bookings);
        }
    }
}
