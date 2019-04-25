using System.ComponentModel.DataAnnotations;

namespace bie.focuscrm.ui.mvc.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}