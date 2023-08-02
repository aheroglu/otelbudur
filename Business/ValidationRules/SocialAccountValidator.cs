using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules
{
    public class SocialAccountValidator : AbstractValidator<SocialAccount>
    {
        public SocialAccountValidator()
        {
            RuleFor(x => x.Link).NotEmpty();
            RuleFor(x => x.Link).Length(20, 100);
        }
    }
}
