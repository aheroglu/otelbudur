using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules
{
    public class AboutValidator : AbstractValidator<About>
    {
        public AboutValidator()
        {
            RuleFor(x => x.Title).NotEmpty().Length(10, 100);

            RuleFor(x => x.Content).NotEmpty().Length(100, 700);

            RuleFor(x => x.PhoneNumber).NotEmpty();

            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }
}
