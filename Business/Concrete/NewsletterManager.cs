using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.EntityFramework;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class NewsletterManager : INewsletterService
    {
        private readonly INewsletterDal _newsletterDal;

        public NewsletterManager(INewsletterDal newsletterDal)
        {
            _newsletterDal = newsletterDal;
        }

        public void TDelete(Newsletter t)
        {
            _newsletterDal.Delete(t);
        }

        public Newsletter TGetById(int id)
        {
            return _newsletterDal.GetById(id);
        }

        public List<Newsletter> TGetList()
        {
            return _newsletterDal.GetList();
        }

        public void TInsert(Newsletter t)
        {
            _newsletterDal.Insert(t);
        }

        public void TUpdate(Newsletter t)
        {
            _newsletterDal.Update(t);
        }
    }
}
