using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules
{
    public class HotelOwnerRequestValidator : AbstractValidator<HotelOwnerRequest>
    {
        public HotelOwnerRequestValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().Length(3, 20);
            RuleFor(x => x.UserName).NotEmpty().Length(8, 15);
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.HotelName).NotEmpty().Length(5, 100);
        }
    }
}
