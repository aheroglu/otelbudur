using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class Question
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Details { get; set; }
        public bool BeenAnswered { get; set; }

        public List<QuestionAnswer> QuestionAnswers { get; set; }
    }
}
