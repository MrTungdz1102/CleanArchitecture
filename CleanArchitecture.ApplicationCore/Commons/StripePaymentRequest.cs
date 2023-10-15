using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.ApplicationCore.Commons
{
    public class StripePaymentRequest
    {
        public string ApprovedUrl { get; set; }
        public string CancelUrl { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
