using CleanArchitecture.WebUI.Models;
using CleanArchitecture.WebUI.Models.DTOs;

namespace CleanArchitecture.WebUI.Services.Interfaces
{
    public interface IBookingService
    {
        Task<ResponseDTO?> CreateBooking(Booking booking);
        Task<ResponseDTO?> UpdateBookingPayment(int bookingId, string sessionId, string paymentIntentId);
        Task<ResponseDTO?> UpdateBookingStatus(int bookingId, string status, int villaNumber);
        Task<ResponseDTO?> GetBooking(int bookingId);
        Task<ResponseDTO?> GetAllBooking(string? status);
        Task<ResponseDTO?> GetAllBookingUser(string? userId, string? status, bool isCustomer);
    }
}
