using System.ComponentModel.DataAnnotations;
using System;

namespace Presentation.Areas.User.Models
{
    public class UserDetailsViewModel
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Image { get; set; }
        public DateTime MemberSince { get; set; }
        public DateTime PasswordLastChange { get; set; }
    }
}
