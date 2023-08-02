using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules
{
    public class HotelValidator : AbstractValidator<Hotel>
    {
        public HotelValidator()
        {
            RuleFor(x => x.Title).NotEmpty().Length(10, 150);
            RuleFor(x => x.Details).Length(10, 3000);
        }
    }
}
