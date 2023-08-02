using System.ComponentModel.DataAnnotations;

namespace Presentation.Areas.Admin.Models.Role
{
    public class AddRoleViewModel
    {
        [Required(ErrorMessage = "'Name' must be not empty!")]
        [MinLength(3, ErrorMessage = "Minimum 3 characters must be enter!")]
        [MaxLength(20, ErrorMessage = "Maximum 20 characters can be entered!")]
        public string RoleName { get; set; }
    }
}
