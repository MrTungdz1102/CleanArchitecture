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
        public BookingSpecification(string? userId, string? status)
        {
            Query.Where(x => (userId == null || x.UserId == userId) && (status == null || x.Status == status) && (!string.IsNullOrEmpty(userId) || !string.IsNullOrEmpty(status)));
        }
        public BookingSpecification(string? status)
        {
            Query.Where(x =>(status == null || x.Status == status) );
        }
    }
}
