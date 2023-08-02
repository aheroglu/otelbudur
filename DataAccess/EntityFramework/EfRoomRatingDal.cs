using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.Repository;
using Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityFramework
{
    public class EfRoomRatingDal : GenericRepository<RoomRating>, IRoomRatingDal
    {
        public List<RoomRating> ReviewsByHotel(int hotelId)
        {
            using (var context = new Context())
            {
                return context.RoomRatings.Include(x => x.User).Include(x => x.Room).Where(x => x.Room.Hotel.Id == hotelId && x.Status == true).OrderByDescending(x => x.Id).ToList();
            }
        }

        public List<RoomRating> ReviewsByRoom(int roomId)
        {
            using (var context = new Context())
            {
                return context.RoomRatings.Include(x => x.User).Include(x => x.Room).Where(x => x.RoomId == roomId).OrderByDescending(x => x.Id).ToList();
            }
        }
    }
}
