using Entity.Concrete;
using System;
using System.Collections.Generic;

namespace Presentation.Models.Booking
{
    public class BookingViewModel
    {
        public int LocationId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public List<Room> Rooms { get; set; }
        public bool FilterParking { get; set; }
        public bool FilterWiFi { get; set; }
        public bool FilterBreakfast { get; set; }
        public bool FilterRoomService { get; set; }
        public bool FilterReception { get; set; }
        public bool FilterPool { get; set; }
        public bool FilterGym { get; set; }
    }
}
