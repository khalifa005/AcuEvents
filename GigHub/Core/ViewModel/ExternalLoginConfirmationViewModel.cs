using System.ComponentModel.DataAnnotations;

namespace GigHub.Core.ViewModel
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}