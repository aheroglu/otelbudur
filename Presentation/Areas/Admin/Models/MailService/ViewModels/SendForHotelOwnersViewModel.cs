using System.ComponentModel.DataAnnotations;

namespace Presentation.Areas.Admin.Models.MailService.ViewModels
{
    public class SendForHotelOwnersViewModel
    {
        [Required]
        [MinLength(5)]
        [MaxLength(200)]
        public string Subject { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(5000)]
        public string Content { get; set; }
    }
}
