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
    public class ContactAnswerManager : IContactAnswerService
    {
        private readonly IContactAnswerDal _contactAnswerDal;

        public ContactAnswerManager(IContactAnswerDal contactAnswerDal)
        {
            _contactAnswerDal = contactAnswerDal;
        }

        public void TDelete(ContactAnswer t)
        {
            _contactAnswerDal.Delete(t);
        }

        public ContactAnswer TGetById(int id)
        {
            return _contactAnswerDal.GetById(id);
        }

        public List<ContactAnswer> TGetList()
        {
            return _contactAnswerDal.GetList();
        }

        public void TInsert(ContactAnswer t)
        {
            _contactAnswerDal.Insert(t);
        }

        public void TUpdate(ContactAnswer t)
        {
            _contactAnswerDal.Update(t);
        }
    }
}
