using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Interfaces.Services;
using CleanArchitecture.Infrastructure.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentAPIController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentAPIController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpPost("CreateCheckout")]
        public IActionResult CreateCheckOut([FromBody] StripePaymentRequest stripePaymentRequest)
        {
            return Ok(_paymentService.CheckOut(stripePaymentRequest));
        }

        [HttpPost("ValidatePayment")]
        public IActionResult ValidatePayment([FromBody]string sessionId)
        {
            return Ok(_paymentService.ValidatePayment(sessionId));
        }

    }
}
