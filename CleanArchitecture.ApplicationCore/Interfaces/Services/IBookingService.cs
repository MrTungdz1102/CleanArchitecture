using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.ApplicationCore.Interfaces.Services
{
    public interface IBookingService
    {
        Task<ResponseDTO> CreateBooking(Booking booking);
        Task<ResponseDTO> GetBooking(int bookingId);
        Task<ResponseDTO> GetAllBookingUser(string userId, string? statusFilter);
        Task<ResponseDTO> GetAllBooking(string? statusFilter);
        Task<ResponseDTO> UpdatePayment(int bookingId, string sessionId, string paymentIntentId);
        Task<ResponseDTO> UpdateStatus(int bookingId, string status, int villaNumber = 0);

    }
}
