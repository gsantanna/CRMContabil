using System.ComponentModel.DataAnnotations;

namespace bie.focuscrm.ui.mvc.Models
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}