using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class AppUser : IdentityUser<int>
    {
        public string FullName { get; set; }
        public string Image { get; set; }
        public DateTime MemberSince { get; set; }
        public DateTime PasswordLastChange { get; set; }
    }
}
