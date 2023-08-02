using DataAccess.Concrete;
using Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using Presentation.Models.MailService;
using System;
using System.Linq;

namespace Presentation.Models.Booking
{
    public class CheckBookingTime
    {
        private readonly Context _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly EvaluateExperienceMailService _evaluateExperienceMailService;

        public CheckBookingTime(Context context, EvaluateExperienceMailService evaluateExperienceMailService, UserManager<AppUser> userManager)
        {
            _context = context;
            _evaluateExperienceMailService = evaluateExperienceMailService;
            _userManager = userManager;
        }

        public void CheckAndUpdate()
        {
            var currentDate = DateTime.Now;
            var roomsToUpdate = _context.Rooms.Where(x => x.ReserveEnding <= currentDate).ToList();

            foreach (var room in roomsToUpdate)
            {
                int reservId = Convert.ToInt32(room.ReservationId);
                var booking = _context.Bookings.FirstOrDefault(x => x.ReservationId == reservId);
                booking.ReservationStatus = "Completed";

                room.ReserveBeginning = null;
                room.ReserveEnding = null;
                room.IsBooked = false;
                room.ReservationId = null;

                string fullName = _userManager.Users.FirstOrDefault(x => x.Id == booking.UserId).FullName;
                string email = _userManager.Users.FirstOrDefault(x => x.Id == booking.UserId).Email;
                _evaluateExperienceMailService.SendMessage(fullName, email, booking.RoomId.ToString());
            }

            _context.SaveChanges();
        }
    }
}
