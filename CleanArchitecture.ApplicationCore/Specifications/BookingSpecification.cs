using Ardalis.Specification;
using CleanArchitecture.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.ApplicationCore.Specifications
{
    public class BookingSpecification : Specification<Booking>
    {
        public BookingSpecification(int bookingId)
        {
            Query.Where(x => x.Id == bookingId);
        }
    }
}
