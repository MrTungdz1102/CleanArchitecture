using CleanArchitecture.WebUI.Models.DTOs;
using CleanArchitecture.WebUI.Models.ViewModel;

namespace CleanArchitecture.WebUI.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<ResponseDTO?> CreateCheckout(StripePaymentRequest stripePaymentRequest);
        Task<ResponseDTO?> ValidatePayment(string sessionId);
        Task<ResponseDTO?> Refund(string paymentIntentId);
    }
}
