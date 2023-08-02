using System;

namespace Presentation.Models.Booking
{
    public class BookViewModel
    {
        public int RoomId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int NumberOfAdult { get; set; }
        public int NumberOfChild { get; set; }
        public double Total { get; set; }
    }
}
