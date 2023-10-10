using CleanArchitecture.WebUI.Models;
using CleanArchitecture.WebUI.Models.DTOs;

namespace CleanArchitecture.WebUI.Services.Interfaces
{
    public interface IBookingService
    {
        Task<ResponseDTO?> CreateBooking(Booking booking);
    }
}
