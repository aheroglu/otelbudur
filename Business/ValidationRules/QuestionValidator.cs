using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules
{
    public class QuestionValidator : AbstractValidator<Question>
    {
        public QuestionValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().Length(4, 30);

            RuleFor(x => x.Email).NotEmpty().Length(10, 30);

            RuleFor(x => x.Details).NotEmpty().Length(10, 500);
        }
    }
}
