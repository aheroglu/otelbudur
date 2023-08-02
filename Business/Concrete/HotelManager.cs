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
    public class HotelManager : IHotelService
    {
        private readonly IHotelDal _hotelDal;

        public HotelManager(IHotelDal hotelDal)
        {
            _hotelDal = hotelDal;
        }

        public void TDelete(Hotel t)
        {
            _hotelDal.Delete(t);
        }

        public Hotel TGetById(int id)
        {
            return _hotelDal.GetById(id);
        }

        public List<Hotel> TGetList()
        {
            return _hotelDal.GetList();
        }

        public List<Hotel> THotelById(int hotelId)
        {
            return _hotelDal.HotelById(hotelId);
        }

        public List<Hotel> THotelByUserId(int id)
        {
            return _hotelDal.HotelByUserId(id);
        }

        public List<Hotel> THotelsByLocation(int id)
        {
            return _hotelDal.HotelsByLocation(id);
        }

        public List<Hotel> THotelsWithLocation()
        {
            return _hotelDal.HotelsWithLocation();
        }

        public void TInsert(Hotel t)
        {
            _hotelDal.Insert(t);
        }

        public void TUpdate(Hotel t)
        {
            _hotelDal.Update(t);
        }
    }
}
