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
    public class HotelOwnerRequestManager : IHotelOwnerRequestService
    {
        private readonly IHotelOwnerRequestDal _hotelOwnerRequestDal;

        public HotelOwnerRequestManager(IHotelOwnerRequestDal hotelOwnerRequestDal)
        {
            _hotelOwnerRequestDal = hotelOwnerRequestDal;
        }

        public void TDelete(HotelOwnerRequest t)
        {
            _hotelOwnerRequestDal.Delete(t);
        }

        public HotelOwnerRequest TGetById(int id)
        {
            return _hotelOwnerRequestDal.GetById(id);
        }

        public List<HotelOwnerRequest> TGetList()
        {
            return _hotelOwnerRequestDal.GetList();
        }

        public void TInsert(HotelOwnerRequest t)
        {
            _hotelOwnerRequestDal.Insert(t);
        }

        public List<HotelOwnerRequest> TListWithLocation()
        {
            return _hotelOwnerRequestDal.ListWithLocation();
        }

        public void TUpdate(HotelOwnerRequest t)
        {
            _hotelOwnerRequestDal.Update(t);
        }
    }
}
