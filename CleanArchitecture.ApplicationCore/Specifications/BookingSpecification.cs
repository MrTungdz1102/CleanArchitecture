using Ardalis.Specification;
using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Entities;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.ApplicationCore.Specifications
{
    public class BookingSpecification : Specification<Booking>
    {
        public BookingSpecification(int bookingId)
        {
            Query.Where(x => x.Id == bookingId).Include(x => x.Villa);
        }
        public BookingSpecification(int bookingId, string paymentStatus)
        {
            Query.Where(x => x.Id == bookingId && x.Status == paymentStatus).Include(x => x.Villa);
        }
        public BookingSpecification(string? userId, string? status, bool isCustomer)
        {
            if(isCustomer)
            {
                Query.Where(x => x.Villa.OwnerId == userId && (status == null || x.Status == status) && (!string.IsNullOrEmpty(userId) || !string.IsNullOrEmpty(status)));
            }
            else
            {
                Query.Where(x => (userId == null || x.UserId == userId) && (status == null || x.Status == status) && (!string.IsNullOrEmpty(userId) || !string.IsNullOrEmpty(status)));
            }
        }
        public BookingSpecification(string? status)
        {
            Query.Where(x =>(status == null || x.Status == status) );
        }
    }
}