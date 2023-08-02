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
    public class QuestionManager : IQuestionService
    {
        private readonly IQuestionDal _questionDal;

        public QuestionManager(IQuestionDal questionDal)
        {
            _questionDal = questionDal;
        }

        public void TDelete(Question t)
        {
            _questionDal.Delete(t);
        }

        public Question TGetById(int id)
        {
            return _questionDal.GetById(id);
        }

        public List<Question> TGetList()
        {
            return _questionDal.GetList();
        }

        public void TInsert(Question t)
        {
            _questionDal.Insert(t);
        }

        public void TUpdate(Question t)
        {
            _questionDal.Update(t);
        }
    }
}
