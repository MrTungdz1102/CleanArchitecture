using Ardalis.Specification;
using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.ApplicationCore.Specifications
{
    public class BookingFilterSpecification : Specification<Booking>
    {
        public BookingFilterSpecification(string firstStatus, string secondStatus)
        {
            Query.Where(x => x.Status == firstStatus || x.Status == secondStatus);
        }
        public BookingFilterSpecification(DateTime date, string firstStatus, string secondStatus)
        {
            Query.Where(u => u.BookingDate >= date.AddDays(-30) &&
           (u.Status != PaymentStatus.StatusPending || u.Status == PaymentStatus.StatusCancelled));
        }
        public BookingFilterSpecification(DateTime startTime, DateTime endTime)
        {
            Query.Where(x =>x.BookingDate >= startTime && x.BookingDate <= endTime);
        }
    }
}
