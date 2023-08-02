using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IHotelService : IGenericService<Hotel>
    {
        public List<Hotel> THotelById(int hotelId);
        public List<Hotel> THotelsWithLocation();
        public List<Hotel> THotelsByLocation(int id);
        public List<Hotel> THotelByUserId(int id);
    }
}
