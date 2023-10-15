using CleanArchitecture.WebUI.Models.DTOs;
using CleanArchitecture.WebUI.Models.ViewModel;
using CleanArchitecture.WebUI.Services.Interfaces;
using CleanArchitecture.WebUI.Utilities;

namespace CleanArchitecture.WebUI.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly IBaseService _baseService;

        public PaymentService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDTO?> CreateCheckout(StripePaymentRequest stripePaymentRequest)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                Url = Constants.APIUrlBase + "/api/PaymentAPI/CreateCheckout",
                ApiType = Constants.ApiType.POST,
                Data = stripePaymentRequest
            });
        }

        public async Task<ResponseDTO?> ValidatePayment(string sessionId)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                Url = Constants.APIUrlBase + "/api/PaymentAPI/ValidatePayment",
                ApiType = Constants.ApiType.POST,
                Data = sessionId
            });
        }
    }
}
