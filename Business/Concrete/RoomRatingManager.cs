using Business.Abstract;
using DataAccess.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class RoomRatingManager : IRoomRatingService
    {
        private readonly IRoomRatingDal _ratingDal;

        public RoomRatingManager(IRoomRatingDal ratingDal)
        {
            _ratingDal = ratingDal;
        }

        public void TDelete(RoomRating t)
        {
            _ratingDal.Delete(t);
        }

        public RoomRating TGetById(int id)
        {
            return _ratingDal.GetById(id);
        }

        public List<RoomRating> TGetList()
        {
            return _ratingDal.GetList();
        }

        public void TInsert(RoomRating t)
        {
            _ratingDal.Insert(t);
        }

        public List<RoomRating> TReviewsByHotel(int hotelId)
        {
            return _ratingDal.ReviewsByHotel(hotelId);
        }

        public List<RoomRating> TReviewsByRoom(int roomId)
        {
            return _ratingDal.ReviewsByRoom(roomId);
        }

        public void TUpdate(RoomRating t)
        {
            _ratingDal.Update(t);
        }
    }
}
