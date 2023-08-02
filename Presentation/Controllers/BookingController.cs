using Business.Concrete;
using DataAccess.Concrete;
using DataAccess.EntityFramework;
using Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.Models.Booking;
using Presentation.Models.MailService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class BookingController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ReservationConfirmedMailService _reservationConfirmedMailService;

        Context context = new Context();
        RoomManager roomManager = new RoomManager(new EfRoomDal());
        BookingManager bookingManager = new BookingManager(new EfBookingDal());

        public BookingController(UserManager<AppUser> userManager, ReservationConfirmedMailService reservationConfirmedMailService)
        {
            _userManager = userManager;
            _reservationConfirmedMailService = reservationConfirmedMailService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index(int locationId, DateTime checkIn, DateTime checkOut, int adultCount, int childCount)
        {
            var values = context.Rooms.Include(x => x.Hotel.Location).Where(x => x.Hotel.LocationId == locationId && x.AdultCount == adultCount && x.ChildCount == childCount && x.IsBooked == false && x.Status == true && ((x.ReserveBeginning == null && x.ReserveEnding == null) || (x.ReserveBeginning > checkOut || x.ReserveEnding < checkIn))).ToList();

            RoomFilterViewModel roomFilterViewModel = new RoomFilterViewModel
            {
                Rooms = values,
                LocationId = locationId,
                AdultCount = adultCount,
                ChildCount = childCount,
                CheckIn = checkIn,
                CheckOut = checkOut
            };

            adult = adultCount;
            child = childCount;
            checkInDate = checkIn;
            checkOutDate = checkOut;

            return View(roomFilterViewModel);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Index(RoomFilterViewModel roomFilterViewModel)
        {
            var filteredRooms = FilterRooms(roomFilterViewModel);
            roomFilterViewModel.Rooms = filteredRooms;
            return View(roomFilterViewModel);
        }

        private List<Room> FilterRooms(RoomFilterViewModel roomFilterViewModel)
        {
            IQueryable<Room> query = context.Rooms.Include(x => x.Hotel.Location).Where(x => x.Hotel.LocationId == roomFilterViewModel.LocationId && x.AdultCount == roomFilterViewModel.AdultCount && x.ChildCount == roomFilterViewModel.ChildCount && x.IsBooked == false && x.Status == true && ((x.ReserveBeginning == null && x.ReserveEnding == null) || (x.ReserveBeginning > roomFilterViewModel.CheckOut || x.ReserveEnding < roomFilterViewModel.CheckIn)));

            if (roomFilterViewModel.Parking)
            {
                query = query.Where(x => x.Parking);
            }

            if (roomFilterViewModel.WiFi)
            {
                query = query.Where(x => x.WiFi);
            }

            if (roomFilterViewModel.Breakfast)
            {
                query = query.Where(x => x.Breakfast);
            }

            if (roomFilterViewModel.RoomService)
            {
                query = query.Where(x => x.RoomService);
            }

            if (roomFilterViewModel.Reception)
            {
                query = query.Where(x => x.Reception);
            }

            if (roomFilterViewModel.Pool)
            {
                query = query.Where(x => x.Pool);
            }

            if (roomFilterViewModel.Gym)
            {
                query = query.Where(x => x.Gym);
            }

            if (roomFilterViewModel.minPrice.HasValue)
            {
                query = query.Where(x => x.Price >= roomFilterViewModel.minPrice);
            }

            if (roomFilterViewModel.maxPrice.HasValue)
            {
                query = query.Where(x => x.Price <= roomFilterViewModel.maxPrice);
            }

            return query.ToList();
        }

        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            var room = roomManager.TRoomDetails(id);
            var roomImages = context.RoomImages.Where(x => x.RoomId == id && x.Status == true).ToList();
            var roomRatings = context.RoomRatings.Include(x => x.User).Where(x => x.RoomId == id && x.Status == true).ToList();

            double ratingSum, result;
            ratingSum = context.RoomRatings.Where(x => x.RoomId == id && x.Status == true).Sum(x => x.Rate);
            result = ratingSum / 5;
            var roomRate = result;

            RoomDetailsViewModel roomDetailsViewModel = new RoomDetailsViewModel
            {
                Room = room,
                RoomImages = roomImages,
                RoomRatings = roomRatings,
                Rate = roomRate
            };

            var roomPrice = roomManager.TGetById(id);

            roomId = id;

            decimal price = (decimal)roomPrice.Price;
            int checkInDay = checkInDate.Day;
            int checkOutDay = checkOutDate.Day;
            decimal totalDay = checkOutDay - checkInDay;
            total = (double)(price * totalDay);

            return View(roomDetailsViewModel);
        }

        public static DateTime checkInDate, checkOutDate;
        public static int adult, child, roomId;
        public static double total;

        [HttpGet]
        public async Task<IActionResult> Book()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var role = await _userManager.GetRolesAsync(user);

            BookViewModel bookViewModel = new BookViewModel()
            {
                RoomId = roomId,
                CheckIn = checkInDate,
                CheckOut = checkOutDate,
                NumberOfAdult = adult,
                NumberOfChild = child,
                Total = total
            };

            if (role.Contains("Member") || role == null)
            {
                if (!user.EmailConfirmed)
                {
                    return RedirectToAction("Index", "ConfirmEmail");
                }

                return View(bookViewModel);
            }

            else
            {
                TempData["ErrorMessage"] = "Only Members Can Reserving!";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Book(bool isConfirmed)
        {
            BookViewModel bookViewModel = new BookViewModel()
            {
                RoomId = roomId,
                CheckIn = checkInDate,
                CheckOut = checkOutDate,
                NumberOfAdult = adult,
                NumberOfChild = child,
                Total = total
            };

            if (bookViewModel.NumberOfAdult <= 0)
            {
                TempData["ErrorMessage"] = "Please fill your booking features!";
                return RedirectToAction("Index", "Home");
            }

            if (!isConfirmed)
            {
                ViewBag.ErrorMessage = "This area should be selected!";
                return View(bookViewModel);
            }

            else
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                Random random = new Random();
                int reservationId = random.Next(100000000, 999999999);

                bool isTaken = context.Bookings.Any(x => x.ReservationId == reservationId);

                if (isTaken)
                {
                    reservationId = random.Next(100000000, 999999999);
                }

                Booking booking = new Booking
                {
                    ReservationId = reservationId,
                    UserId = user.Id,
                    BookingTime = DateTime.Now,
                    RoomId = roomId,
                    CheckIn = checkInDate,
                    CheckOut = checkOutDate,
                    AdultCount = adult,
                    ChildCount = child,
                    ReservationStatus = "Pending",
                    Total = (decimal)total
                };
                bookingManager.TInsert(booking);

                _reservationConfirmedMailService.SendMail(user.FullName, user.Email, reservationId);

                return RedirectToAction("Confirmation");
            }
        }

        public async Task<IActionResult> Confirmation()
        {
            if (!string.IsNullOrEmpty(User.Identity.Name))
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (user != null)
                {
                    ViewBag.FullName = context.Users.FirstOrDefault(x => x.Id == user.Id).FullName;
                }
            }

            return View();
        }

    }
}
