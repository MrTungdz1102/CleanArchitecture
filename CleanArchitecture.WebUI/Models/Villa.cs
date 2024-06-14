using LazZiya.ExpressLocalization.DataAnnotations;
using LazZiya.ExpressLocalization.Messages;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArchitecture.WebUI.Models
{
    public class Villa
    {
        public int Id { get; set; }

        [ExRequired]
        [ExStringLength(50)]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Display(Name = "Price per night")]
        [ExRequired(ErrorMessage = DataAnnotationsErrorMessages.RequiredAttribute_ValidationError)]
        public double Price { get; set; }
        [Required]
        public int SquareFeet { get; set; }
        [Range(1, 10)]
        public int Occupancy { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageLocalPath { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }

        [ValidateNever]
        public IEnumerable<Amenity>? VillaAmenity { get; set; }
        public bool IsAvailable { get; set; }

        public double StartRating { get; set; }
        public string? UserId { get; set; }

        public IEnumerable<Review>? ReviewList { get; set; }

        public int CityId { get; set; }
        public City? City { get; set; }

        [ValidateNever]
        [NotMapped]
        public string? ReviewContent { get; set; }
    }
}
