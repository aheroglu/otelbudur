using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IRoomRatingService : IGenericService<RoomRating>
    {
        public List<RoomRating> TReviewsByHotel(int hotelId);
        public List<RoomRating> TReviewsByRoom(int roomId);
    }
}
