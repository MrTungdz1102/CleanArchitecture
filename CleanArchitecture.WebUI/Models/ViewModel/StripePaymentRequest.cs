namespace CleanArchitecture.WebUI.Models.ViewModel
{
    public class StripePaymentRequest
    {
        public string ApprovedUrl { get; set; }
        public string CancelUrl { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
