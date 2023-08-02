using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class Room
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CoverImage { get; set; }
        public string About { get; set; }
        public double Price { get; set; }
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public bool Parking { get; set; }
        public bool WiFi { get; set; }
        public bool Breakfast { get; set; }
        public bool RoomService { get; set; }
        public bool Reception { get; set; }
        public bool Pool { get; set; }
        public bool Gym { get; set; }
        public bool IsBooked { get; set; }
        public DateTime? ReserveBeginning { get; set; }
        public DateTime? ReserveEnding { get; set; }
        public int? ReservationId { get; set; }
        public int Rating { get; set; }
        public bool Status { get; set; }

        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }

        public List<RoomImage> RoomImages { get; set; }
        public List<Booking> Bookings { get; set; }
        public List<RoomRating> Ratings { get; set; }
    }
}