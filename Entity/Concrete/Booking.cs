using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class Booking
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public DateTime BookingTime { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public decimal Total { get; set; }
        public string ReservationStatus { get; set; }

        public int UserId { get; set; }
        public AppUser User { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}
