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
    public class QuestionAnswerManager : IQuestionAnswerService
    {
        private readonly IQuestionAnswerDal _questionAnswerDal;

        public QuestionAnswerManager(IQuestionAnswerDal questionAnswerDal)
        {
            _questionAnswerDal = questionAnswerDal;
        }

        public void TDelete(QuestionAnswer t)
        {
            _questionAnswerDal.Delete(t);
        }

        public QuestionAnswer TGetById(int id)
        {
            return _questionAnswerDal.GetById(id);
        }

        public List<QuestionAnswer> TGetList()
        {
            return _questionAnswerDal.GetList();
        }

        public void TInsert(QuestionAnswer t)
        {
            _questionAnswerDal.Insert(t);
        }

        public void TUpdate(QuestionAnswer t)
        {
            _questionAnswerDal.Update(t);
        }
    }
}
