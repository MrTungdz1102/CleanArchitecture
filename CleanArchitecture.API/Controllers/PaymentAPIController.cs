﻿using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public IActionResult ValidatePayment([FromBody] string sessionId)
        {
            return Ok(_paymentService.ValidatePayment(sessionId));
        }

        [HttpPost("Refund")]
        public IActionResult Refund([FromBody] string paymentIntentId)
        {
            return Ok(_paymentService.Refund(paymentIntentId));
        }

    }
}
