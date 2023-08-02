using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IRoomRatingDal : IGenericDal<RoomRating>
    {
        public List<RoomRating> ReviewsByHotel(int hotelId);
        public List<RoomRating> ReviewsByRoom(int roomId);
    }
}
