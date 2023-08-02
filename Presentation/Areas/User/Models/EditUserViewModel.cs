using System;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Areas.User.Models
{
    public class EditUserViewModel
    {
        [Required(ErrorMessage = "'Full Name' must not be empty.")]
        [MinLength(4, ErrorMessage = "Minimum 4 characters must be enter!")]
        [MaxLength(30, ErrorMessage = "Maximum 30 characters can be entered!")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "'User Name' must not be empty.")]
        [MinLength(8, ErrorMessage = "Minimum 8 characters must be enter!")]
        [MaxLength(15, ErrorMessage = "Maximum 15 characters can be entered!")] 
        public string UserName { get; set; }
        [Required(ErrorMessage = "'Email' must not be empty.")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "'Phone Number' must not be empty.")]
        public string PhoneNumber { get; set; }
    }
}
