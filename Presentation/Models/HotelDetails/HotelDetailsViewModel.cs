using Entity.Concrete;
using System.Collections.Generic;

namespace Presentation.Models.HotelDetails
{
    public class HotelDetailsViewModel
    {
        public List<Hotel> Hotel { get; set; }
        public List<Room> Rooms { get; set; }
        public List<RoomRating> Reviews { get; set; }
    }
}