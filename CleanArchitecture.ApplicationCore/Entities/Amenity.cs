using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArchitecture.ApplicationCore.Entities
{
    public class Amenity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        [ForeignKey("Villa")]
        public int VillaId { get; set; }
        public Villa? Villa { get; set; }
    }
}
