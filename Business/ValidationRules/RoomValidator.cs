using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules
{
    public class RoomValidator : AbstractValidator<Room>
    {
        public RoomValidator()
        {
            RuleFor(x => x.Title).NotEmpty().Length(5, 100);

            RuleFor(x => x.About).Length(5, 5000);

            RuleFor(x => x.Price).NotEmpty();
        }
    }
}
