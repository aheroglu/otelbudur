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
    public class SocialAccountManager : ISocialAccountService
    {
        private readonly ISocialAccountDal _socialAccountDal;

        public SocialAccountManager(ISocialAccountDal socialAccountDal)
        {
            _socialAccountDal = socialAccountDal;
        }

        public void TDelete(SocialAccount t)
        {
            _socialAccountDal.Delete(t);
        }

        public SocialAccount TGetById(int id)
        {
            return _socialAccountDal.GetById(id);
        }

        public List<SocialAccount> TGetList()
        {
            return _socialAccountDal.GetList();
        }

        public void TInsert(SocialAccount t)
        {
            _socialAccountDal.Insert(t);
        }

        public void TUpdate(SocialAccount t)
        {
            _socialAccountDal.Update(t);
        }
    }
}
