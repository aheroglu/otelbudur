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
    public class EfRoomDal : GenericRepository<Room>, IRoomDal
    {
        public List<Room> RoomDetails(int id)
        {
            using (var context = new Context())
            {
                return context.Rooms.Include(x => x.Hotel.Location).Where(x => x.Id == id).ToList();
            }
        }

        public List<Room> RoomsByHotel(int id)
        {
            using (var context = new Context())
            {
                return context.Rooms.Include(x => x.Hotel.Location).Include(x => x.RoomImages).Include(x => x.Ratings).Where(x => x.HotelId == id && x.Status == true).OrderByDescending(x => x.Id).ToList();
            }
        }
    }
}
