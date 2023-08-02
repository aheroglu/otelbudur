using System.ComponentModel.DataAnnotations;

namespace Presentation.Models.ConfirmEmail
{
    public class ConfirmEmailViewModel
    {
        [Required(ErrorMessage = "'Email' must not be empty.")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
