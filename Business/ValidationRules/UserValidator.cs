using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules
{
    public class UserValidator : AbstractValidator<AppUser>
    {
        public UserValidator()
        {
            RuleFor(x => x.FullName).NotEmpty();
            RuleFor(x => x.FullName).Length(4, 30);

            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.UserName).Length(8, 15);

            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Email).Length(10, 50);

            RuleFor(x => x.PhoneNumber).NotEmpty();
        }
    }
}
