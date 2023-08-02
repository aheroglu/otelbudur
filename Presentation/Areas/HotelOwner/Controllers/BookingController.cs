using Business.Concrete;
using DataAccess.Concrete;
using DataAccess.EntityFramework;
using Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Areas.HotelOwner.Models.MailService.Booking;
using System;
using System.Linq;
using X.PagedList;

namespace Presentation.Areas.HotelOwner.Controllers
{
    [Area("HotelOwner")]
    [Authorize(Roles = "Hotel Owner")]
    public class BookingController : Controller
    {
        private readonly Context _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly HotelOwnerAcceptBookingMailService _hotelOwnerAcceptBookingMailService;
        private readonly HotelOwnerRejectBookingMailService _hotelOwnerRejectBookingMailService;

        BookingManager bookingManager = new BookingManager(new EfBookingDal());
        RoomManager roomManager = new RoomManager(new EfRoomDal());

        public BookingController(Context context, UserManager<AppUser> userManager, HotelOwnerRejectBookingMailService hotelOwnerRejectBookingMailService, HotelOwnerAcceptBookingMailService hotelOwnerAcceptBookingMailService)
        {
            _context = context;
            _userManager = userManager;
            _hotelOwnerRejectBookingMailService = hotelOwnerRejectBookingMailService;
            _hotelOwnerAcceptBookingMailService = hotelOwnerAcceptBookingMailService;
        }

        public IActionResult Index(int page = 1)
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }

            int userId = Convert.ToInt32(_userManager.GetUserId(User));
            var hotelId = _context.Hotels.FirstOrDefault(x => x.UserId == userId).Id;
            var bookings = bookingManager.TLatestBookingsOfTheHotel(hotelId).ToPagedList(page, 10);
            return View(bookings);
        }

        public IActionResult SaveAsAccepted(int id)
        {
            var booking = bookingManager.TGetById(id);
            booking.ReservationStatus = "Accepted";
            bookingManager.TUpdate(booking);

            var room = roomManager.TGetById(booking.RoomId);
            room.ReserveBeginning = booking.CheckIn;
            room.ReserveEnding = booking.CheckOut;
            room.IsBooked = true;
            room.ReservationId = booking.ReservationId;
            roomManager.TUpdate(room);

            string fullName = _userManager.Users.FirstOrDefault(x => x.Id == booking.UserId).FullName;
            string email = _userManager.Users.FirstOrDefault(x => x.Id == booking.UserId).Email;
            _hotelOwnerAcceptBookingMailService.SendMessage(fullName, email);

            TempData["SuccessMessage"] = "Booking Saved As Accepted!";
            return RedirectToAction("Booking", "HotelOwner");
        }

        public IActionResult SaveAsCancelled(int id)
        {
            var booking = bookingManager.TGetById(id);
            booking.ReservationStatus = "Cancelled";
            bookingManager.TUpdate(booking);

            string fullName = _userManager.Users.FirstOrDefault(x => x.Id == booking.UserId).FullName;
            string email = _userManager.Users.FirstOrDefault(x => x.Id == booking.UserId).Email;
            _hotelOwnerRejectBookingMailService.SendMessage(fullName, email);

            TempData["SuccessMessage"] = "Booking Saved As Cancelled!";
            return RedirectToAction("Booking", "HotelOwner");
        }
    }
}
