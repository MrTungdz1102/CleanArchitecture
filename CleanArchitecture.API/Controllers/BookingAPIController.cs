using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Entities;
using CleanArchitecture.ApplicationCore.Interfaces.Services;
using CleanArchitecture.ApplicationCore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingAPIController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingAPIController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost("CreateBooking")]
        public async Task<ActionResult<ResponseDTO>> CreateBooking([FromBody] Booking booking)
        {
            return Ok(await _bookingService.CreateBooking(booking));
        }
    }
}
