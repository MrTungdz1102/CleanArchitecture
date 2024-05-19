using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.WebUI.Models
{
    public class Coupon
    {
        public int CouponId { get; set; }
        [StringLength(5)]
        public string CouponCode { get; set; }
        public double DiscountAmount { get; set; }
        public int MinAmount { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
    }
}
