using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IHotelDal : IGenericDal<Hotel>
    {
        public List<Hotel> HotelById(int hotelId);
        public List<Hotel> HotelsWithLocation();
        public List<Hotel> HotelsByLocation(int id);
        public List<Hotel> HotelByUserId(int id);
    }
}
