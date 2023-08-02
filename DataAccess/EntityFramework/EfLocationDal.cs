using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.Repository;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityFramework
{
    public class EfLocationdal : GenericRepository<Location>, ILocationDal
    {
        public List<Location> LocationListAscName()
        {
            using (var context = new Context())
            {
                return context.Locations.OrderBy(x => x.Name).ToList();
            }
        }
    }
}
