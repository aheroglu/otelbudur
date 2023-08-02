using System.ComponentModel.DataAnnotations;

namespace Presentation.Models.MailService
{
    public class ForgotPasswordMailService
    {
        [Required(ErrorMessage = "'Email' must not be empty.")]
        [EmailAddress]
        public string Receiver { get; set; }
    }
}
