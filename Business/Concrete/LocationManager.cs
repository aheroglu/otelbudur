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
    public class LocationManager : ILocationService
    {
        private readonly ILocationDal _locationDal;

        public LocationManager(ILocationDal locationDal)
        {
            _locationDal = locationDal;
        }

        public void TDelete(Location t)
        {
            _locationDal.Delete(t);
        }

        public Location TGetById(int id)
        {
            return _locationDal.GetById(id);
        }

        public List<Location> TGetList()
        {
            return _locationDal.GetList();
        }

        public void TInsert(Location t)
        {
            _locationDal.Insert(t);
        }

        public List<Location> TLocationListAscName()
        {
            return _locationDal.LocationListAscName();
        }

        public void TUpdate(Location t)
        {
            _locationDal.Update(t);

        }
    }
}
