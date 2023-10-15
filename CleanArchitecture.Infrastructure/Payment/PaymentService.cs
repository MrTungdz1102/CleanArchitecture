using Azure;
using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Interfaces.Services;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Payment
{
    public class PaymentService : IPaymentService
    {
        private ResponseDTO _response;
        public PaymentService()
        {
            _response = new ResponseDTO();
        }
        public ResponseDTO CheckOut(StripePaymentRequest stripePaymentRequest)
        {
            try
            {
                var options = new SessionCreateOptions
                {
                    SuccessUrl = stripePaymentRequest.ApprovedUrl,
                    CancelUrl = stripePaymentRequest.CancelUrl,
                    Mode = "payment",
                    LineItems = new List<SessionLineItemOptions>()
                };
                options.LineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(stripePaymentRequest.Price * 100),
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = stripePaymentRequest.Name
                        },
                    },
                    Quantity = 1,
                });

                SessionService sessionService = new SessionService();
                Session session = sessionService.Create(options);
                PaymentResponse paymentResponse = new PaymentResponse
                {
                    StripeSessionUrl = session.Url,
                    StripeSessionId = session.Id,
                    PaymentIntentId = session.PaymentIntentId
                };
                _response.Result = paymentResponse;
            }
            catch(Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return _response;
        }

        public ResponseDTO ValidatePayment(string sessionId)
        {
            try
            {
                SessionService service = new SessionService();
                Session session = service.Get(sessionId);
                if (session.PaymentStatus == "paid")
                {
                    PaymentResponse paymentResponse = new PaymentResponse
                    {
                        StripeSessionId = session.Id,
                        PaymentIntentId = session.PaymentIntentId
                    };
                    _response.Result = paymentResponse;
                    return _response;
                }
                _response.IsSuccess = false;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return _response;
        }
    }
}
