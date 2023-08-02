using Entity.Concrete;
using System;
using System.Collections.Generic;

namespace Presentation.Models.Booking
{
    public class RoomFilterViewModel
    {
        public List<Room> Rooms { get; set; }

        public int LocationId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public bool Parking { get; set; }
        public bool WiFi { get; set; }
        public bool Breakfast { get; set; }
        public bool RoomService { get; set; }
        public bool Reception { get; set; }
        public bool Pool { get; set; }
        public bool Gym { get; set; }
        public double? minPrice { get; set; }
        public double? maxPrice { get; set; }
    }
}
