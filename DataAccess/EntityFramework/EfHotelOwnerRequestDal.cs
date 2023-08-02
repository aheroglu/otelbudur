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
    public class EfHotelOwnerRequestDal : GenericRepository<HotelOwnerRequest>, IHotelOwnerRequestDal
    {
        public List<HotelOwnerRequest> ListWithLocation()
        {
            using (var context = new Context())
            {
                return context.HotelOwnerRequests.Include(x => x.Location).OrderByDescending(x => x.Id).ToList();
            }
        }
    }
}
