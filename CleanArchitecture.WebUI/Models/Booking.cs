using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.WebUI.Models
{
    public class Booking
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public AppUser? AppUser { get; set; }

        public int VillaId { get; set; }
        public Villa? Villa { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public double TotalCost { get; set; }
        public int Nights { get; set; }
        public string Status { get; set; }

        public DateTime BookingDate { get; set; }
        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }

        public bool IsPaymentSuccessful { get; set; } = false;
        public DateTime PaymentDate { get; set; }

        public string? StripeSessionId { get; set; }
        public string? StripePaymentIntentId { get; set; }

        public DateTime ActualCheckInDate { get; set; }
        public DateTime ActualCheckOutDate { get; set; }

        public string? CouponCode { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "The Participants must is an integer number")]
        public int Participants { get; set; }

        public int VillaNumber { get; set; }
        [ValidateNever]
        public ICollection<VillaNumber>? VillaNumbers { get; set; }
    }
}
