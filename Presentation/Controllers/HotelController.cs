using Business.Concrete;
using DataAccess.EntityFramework;
using Entity.Concrete;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.HotelDetails;
using System.Collections.Generic;

namespace Presentation.Controllers
{
    public class HotelController : Controller
    {
        HotelManager hotelManager = new HotelManager(new EfHotelDal());
        RoomManager roomManager = new RoomManager(new EfRoomDal());
        RoomRatingManager roomRatingManager = new RoomRatingManager(new EfRoomRatingDal());

        public IActionResult Index(int id)
        {
            var hotel = hotelManager.THotelById(id);
            var rooms = roomManager.TRoomsByHotel(id);
            var reviews = roomRatingManager.TReviewsByHotel(id);

            HotelDetailsViewModel hotelDetailsViewModel = new HotelDetailsViewModel
            {
                Hotel = hotel,
                Rooms = rooms,
                Reviews = reviews
            };

            return View(hotelDetailsViewModel);
        }
    }
}
