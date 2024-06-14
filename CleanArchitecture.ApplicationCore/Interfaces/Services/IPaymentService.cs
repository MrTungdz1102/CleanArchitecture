using CleanArchitecture.ApplicationCore.Commons;

namespace CleanArchitecture.ApplicationCore.Interfaces.Services
{
    public interface IPaymentService
    {
        ResponseDTO CheckOut(StripePaymentRequest stripePaymentRequest);
        ResponseDTO ValidatePayment(string sessionId);
        ResponseDTO Refund(string paymentIntentId);
    }
}
