using Business.Concrete;
using DataAccess.Concrete;
using DataAccess.EntityFramework;
using Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using X.PagedList;

namespace Presentation.Areas.HotelOwner.Controllers
{
    [Area("HotelOwner")]
    [Authorize(Roles = "Hotel Owner")]
    public class DashboardController : Controller
    {
        private readonly Context _context;
        private readonly UserManager<AppUser> _userManager;

        BookingManager bookingManager = new BookingManager(new EfBookingDal());

        public DashboardController(Context context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index(int page = 1)
        {
            int userId = Convert.ToInt32(_userManager.GetUserId(User));
            int hotelId = _context.Hotels.FirstOrDefault(x => x.UserId == userId).Id;

            ViewBag.PendingReservationCount = bookingManager.TPendingReservationCount(hotelId);
            ViewBag.AcceptedReservationCount = bookingManager.TAcceptedReservationCount(hotelId);
            ViewBag.CancelledReservationCount = bookingManager.TCancelledReservationCount(hotelId);
            ViewBag.CompletedReservationCount = bookingManager.TCompletedReservationCount(hotelId);

            var values = bookingManager.TLatestBookingsOfTheHotel(hotelId).ToPagedList(page, 5);
            return View(values);
        }
    }
}
