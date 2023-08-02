using System.ComponentModel.DataAnnotations;

namespace Presentation.Models.HotelOwnerLogIn
{
    public class HotelOwnerLogInViewModel
    {
        [Required(ErrorMessage = "'User Name' must not be empty.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "'Password' must not be empty.")]
        public string Password { get; set; }
    }
}
