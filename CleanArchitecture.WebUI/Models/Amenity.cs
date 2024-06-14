using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.WebUI.Models
{
    public class Amenity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public int VillaId { get; set; }
        public Villa? Villa { get; set; }
    }
}
