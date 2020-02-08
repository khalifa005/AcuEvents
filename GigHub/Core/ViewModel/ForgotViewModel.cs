using System.ComponentModel.DataAnnotations;

namespace GigHub.Core.ViewModel
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}