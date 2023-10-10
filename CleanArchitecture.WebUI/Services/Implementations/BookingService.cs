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
    }
}
