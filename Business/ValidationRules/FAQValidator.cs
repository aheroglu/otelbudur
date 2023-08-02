using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules
{
    public class FAQValidator : AbstractValidator<FAQ>
    {
        public FAQValidator()
        {
            RuleFor(x => x.Title).NotEmpty().Length(10, 50);

            RuleFor(x => x.Description).NotEmpty().Length(10, 500);
        }
    }
}
