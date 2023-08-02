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
    public class FAQManager : IFAQService
    {
        private readonly IFAQDal _FAQDal;

        public FAQManager(IFAQDal fAQDal)
        {
            _FAQDal = fAQDal;
        }

        public void TDelete(FAQ t)
        {
            _FAQDal.Delete(t);
        }

        public FAQ TGetById(int id)
        {
            return _FAQDal.GetById(id);
        }

        public List<FAQ> TGetList()
        {
            return _FAQDal.GetList();
        }

        public void TInsert(FAQ t)
        {
            _FAQDal.Insert(t);
        }

        public void TUpdate(FAQ t)
        {
            _FAQDal.Update(t);
        }
    }
}
