﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArchitecture.ApplicationCore.Entities.DTOs
{
    public class VillaDTO
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
        public string? ImageUrl { get; set; }
        public string? ImageLocalPath { get; set; }
        [NotMapped]
        public IFormFile? Image { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }

        public virtual ICollection<Amenity> VillaAmenity { get; set; }
        public bool IsAvailable { get; set; } = true;

        public double StartRating { get; set; }
        public string? UserId { get; set; }

        public int CityId { get; set; }
        public City? City { get; set; }
    }
}
