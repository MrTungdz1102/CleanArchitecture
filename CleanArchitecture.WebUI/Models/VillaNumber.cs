using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.WebUI.Models
{
    public class VillaNumber
    {
        [Required]
        public int Villa_Number { get; set; }
        public int VillaId { get; set; }
        public Villa? Villa { get; set; }
        public string? SpecialDetails { get; set; }
    }
}
