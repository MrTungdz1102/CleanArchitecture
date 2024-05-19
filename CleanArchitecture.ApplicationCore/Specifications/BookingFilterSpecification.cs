using Ardalis.Specification;
using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Entities;

namespace CleanArchitecture.ApplicationCore.Specifications
{
    public class BookingFilterSpecification : Specification<Booking>
    {
        public BookingFilterSpecification(string firstStatus, string secondStatus, string? ownerId = null)
        {
            if (ownerId is not null)
            {
                Query.Include(x => x.Villa).Where(x => (x.Status != firstStatus || x.Status != secondStatus) && x.Villa.OwnerId == ownerId);
            }
            else
            {
                Query.Where(x => x.Status != firstStatus || x.Status != secondStatus);
            }
        }
        public BookingFilterSpecification(DateTime date, string firstStatus, string secondStatus, string? ownerId = null)
        {
            if (ownerId is not null)
            {
                Query.Include(x => x.Villa).Where(u => u.BookingDate >= date.AddDays(-30) &&
                                            (u.Status != PaymentStatus.StatusPending 
                                            || u.Status == PaymentStatus.StatusCancelled) &&                          u.Villa!.OwnerId == ownerId);
            }
            else
            {
                Query.Where(u => u.BookingDate >= date.AddDays(-30) &&
                        (u.Status != PaymentStatus.StatusPending 
                        || u.Status == PaymentStatus.StatusCancelled));
            }
        }
        public BookingFilterSpecification(DateTime startTime, DateTime endTime, string? ownerId = null)
        {
            if (ownerId is not null)
            {
                Query.Include(x => x.Villa).Where(x => (x.BookingDate >= startTime && x.BookingDate <= endTime) && x.Villa!.OwnerId == ownerId);
            }
            else
            {
                Query.Where(x => x.BookingDate >= startTime && x.BookingDate <= endTime);
            }  
        }
    }
}
