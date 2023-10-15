using CleanArchitecture.ApplicationCore.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.ApplicationCore.Interfaces.Services
{
    public interface IPaymentService
    {
        ResponseDTO CheckOut(StripePaymentRequest stripePaymentRequest);
        ResponseDTO ValidatePayment(string sessionId);
    }
}
