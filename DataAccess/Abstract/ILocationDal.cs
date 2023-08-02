using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ILocationDal : IGenericDal<Location>
    {
        public List<Location> LocationListAscName();
    }
}
