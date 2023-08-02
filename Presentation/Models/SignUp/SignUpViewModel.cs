using System;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Models.SignUp
{
    public class SignUpViewModel
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
        public string Image { get; set; }
        [Required(ErrorMessage = "'Password' must not be empty.")]
        [MinLength(8, ErrorMessage = "Minimum 8 characters must be enter!")]
        [MaxLength(15, ErrorMessage = "Maximum 15 characters can be entered!")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\w).*$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number and one alphanumeric character!")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Paswords Do Not Match!")]
        public string ConfirmPassword { get; set; }
    }
}