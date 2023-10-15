using CleanArchitecture.WebUI.Models;
using CleanArchitecture.WebUI.Models.DTOs;
using CleanArchitecture.WebUI.Services.Interfaces;
using CleanArchitecture.WebUI.Utilities;

namespace CleanArchitecture.WebUI.Services.Implementations
{
    public class BookingService : IBookingService
    {
        private readonly IBaseService _baseService;

        public BookingService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDTO?> CreateBooking(Booking booking)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                Url = Constants.APIUrlBase + "/api/BookingAPI/CreateBooking",
                Data = booking,
                ApiType = Constants.ApiType.POST
            });
        }

        public async Task<ResponseDTO?> GetAllBooking()
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                Url = Constants.APIUrlBase + "/api/BookingAPI/GetAllBooking",
                ApiType = Constants.ApiType.GET
            });
        }

        public async Task<ResponseDTO?> GetBooking(int bookingId)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                Url = Constants.APIUrlBase + "/api/BookingAPI/GetBooking/" + bookingId,
                ApiType = Constants.ApiType.GET
            });
        }

        public async Task<ResponseDTO?> UpdateBookingPayment(int bookingId, string sessionId, string paymentIntentId)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                Url = Constants.APIUrlBase + $"/api/BookingAPI/UpdateBookingPayment/{bookingId}?sessionId={sessionId}&paymentIntentId={paymentIntentId}",
                ApiType = Constants.ApiType.PUT
            });
        }

        public async Task<ResponseDTO?> UpdateBookingStatus(int bookingId, string status)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                Url = Constants.APIUrlBase + $"/api/BookingAPI/UpdateBookingStatus/{bookingId}?status={status}",
                ApiType = Constants.ApiType.PUT
            });
        }
    }
}
