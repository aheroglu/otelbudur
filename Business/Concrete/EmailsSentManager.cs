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
    public class EmailsSentManager : IEmailsSentService
    {
        private readonly IEmailsSentDal _emailsSentDal;

        public EmailsSentManager(IEmailsSentDal emailsSentDal)
        {
            _emailsSentDal = emailsSentDal;
        }

        public void TDelete(EmailsSent t)
        {
            _emailsSentDal.Delete(t);
        }

        public EmailsSent TGetById(int id)
        {
            return _emailsSentDal.GetById(id);
        }

        public List<EmailsSent> TGetList()
        {
            return _emailsSentDal.GetList();
        }

        public void TInsert(EmailsSent t)
        {
            _emailsSentDal.Insert(t);
        }

        public void TUpdate(EmailsSent t)
        {
            _emailsSentDal.Update(t);
        }
    }
}
