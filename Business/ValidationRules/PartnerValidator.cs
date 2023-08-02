using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules
{
    public class PartnerValidator : AbstractValidator<Partner>
    {
        public PartnerValidator()
        {
            RuleFor(x => x.DestinationUrl).NotEmpty();
        }
    }
}
