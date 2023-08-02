using Entity.Concrete;
using System.Collections.Generic;

namespace Presentation.Models.Booking
{
    public class RoomDetailsViewModel
    {
        public List<Room> Room { get; set; }
        public List<RoomImage> RoomImages { get; set; }
        public List<RoomRating> RoomRatings { get; set; }
        public double Rate { get; set; }
    }
}
