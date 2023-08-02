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
    public class PartnerManager : IPartnerService
    {
        private readonly IPartnerDal _partnerDal;

        public PartnerManager(IPartnerDal partnerDal)
        {
            _partnerDal = partnerDal;
        }

        public void TDelete(Partner t)
        {
            _partnerDal.Delete(t);
        }

        public Partner TGetById(int id)
        {
            return _partnerDal.GetById(id);
        }

        public List<Partner> TGetList()
        {
            return _partnerDal.GetList();
        }

        public void TInsert(Partner t)
        {
            _partnerDal.Insert(t);
        }

        public void TUpdate(Partner t)
        {
            _partnerDal.Update(t);
        }
    }
}
