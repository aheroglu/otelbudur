using Entity.Concrete;
using System.Collections.Generic;

namespace DataAccess.Abstract
{
    public interface IHotelOwnerRequestDal : IGenericDal<HotelOwnerRequest>
    {
        public List<HotelOwnerRequest> ListWithLocation();
    }
}
