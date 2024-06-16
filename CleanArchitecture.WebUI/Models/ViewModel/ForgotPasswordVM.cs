using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.WebUI.Models.ViewModel
{
    public class ForgotPasswordVM
    {
        [Required]
        public string Email { get; set; }
    }
}
