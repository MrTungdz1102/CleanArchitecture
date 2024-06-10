using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArchitecture.ApplicationCore.Entities
{
    [Table("HotelRooms")]
    public class VillaNumber
    {
        // khong set identity cho villa number, so phong (villa) phai khac nhau
        // vi du 101, 102, 103
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Hotel Room")]
        public int Villa_Number { get; set; }

        [ForeignKey("Villa")]
        public int VillaId { get; set; }
        public Villa? Villa { get; set; }
        public string? SpecialDetails { get; set; }
        //public int AdultCapacity { get; set; }
        //public int ChildCapacity { get; set; }
        //   public double PricePerNight { get; set; }
    }
}
