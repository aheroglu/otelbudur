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
    public class EfHotelDal : GenericRepository<Hotel>, IHotelDal
    {
        public List<Hotel> HotelById(int hotelId)
        {
            using (var context = new Context())
            {
                return context.Hotels.Include(x => x.Location).Where(x => x.Id == hotelId).ToList();
            }

        }

        public List<Hotel> HotelByUserId(int id)
        {
            using (var context = new Context())
            {
                return context.Hotels.Include(x => x.Location).Where(x => x.UserId == id).ToList();
            }
        }

        public List<Hotel> HotelsByLocation(int id)
        {
            using (var context = new Context())
            {
                return context.Hotels.Include(x => x.Location).Include(x => x.User).Where(x => x.LocationId == id && x.Status == true).OrderBy(x => x.Title).ToList();
            }
        }

        public List<Hotel> HotelsWithLocation()
        {
            using (var context = new Context())
            {
                return context.Hotels.Include(x => x.Location).Include(x => x.User).Where(x => x.Status == true).OrderBy(x => x.Title).ToList();
            }
        }
    }
}
