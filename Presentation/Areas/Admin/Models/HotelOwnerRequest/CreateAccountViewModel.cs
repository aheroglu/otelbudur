using System.ComponentModel.DataAnnotations;

namespace Presentation.Areas.Admin.Models.HotelOwnerRequest
{
    public class CreateAccountViewModel
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string HotelName { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "'Password' must not be empty.")]
        [MinLength(8, ErrorMessage = "Minimum 8 characters must be enter!")]
        [MaxLength(15, ErrorMessage = "Maximum 15 characters can be entered!")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\w).*$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number and one alphanumeric character!")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Paswords Do Not Match!")]
        public string ConfirmPassword { get; set; }
    }
}