using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.WebUI.Models.DTOs
{
    public class ChangePasswordDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string CurrentPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword))]
        [Display(Name = "Confirm New password")]
        public string ConfirmNewPassword { get; set; }
    }
}
