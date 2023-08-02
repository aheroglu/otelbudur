using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules
{
    public class ContactValidator : AbstractValidator<Contact>
    {
        public ContactValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().Length(2, 20);

            RuleFor(x => x.LastName).NotEmpty().Length(2, 20);

            RuleFor(x => x.Email).NotEmpty().Length(10, 30);

            RuleFor(x => x.PhoneNumber).NotEmpty();

            RuleFor(x => x.Message).NotEmpty().Length(10, 500);
        }
    }
}
