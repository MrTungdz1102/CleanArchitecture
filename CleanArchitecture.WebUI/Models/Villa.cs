using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.WebUI.Models
{
    public class Villa
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Display(Name = "Price per night")]
        public double Price { get; set; }
        public int SquareFeet { get; set; }
        [Range(1, 10)]
        public int Occupancy { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }

        [ValidateNever]
        public IEnumerable<Amenity>? VillaAmenity { get; set; }
        public bool IsAvailable { get; set; }
    }
}
