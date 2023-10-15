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

        [HttpPut("UpdateBookingPayment/{bookingId:int}")]
        public async Task<ActionResult<ResponseDTO>> UpdateBookingPayment([FromRoute] int bookingId, string sessionId, string paymentIntentId)
        {
            return Ok(await _bookingService.UpdatePayment(bookingId, sessionId, paymentIntentId));
        }

        [HttpGet("GetAllBooking")]
        public async Task<ActionResult<ResponseDTO>> GetAllBooking()
        {
            return Ok(await _bookingService.GetAllBooking());
        }

        [HttpGet("GetBooking/{bookingId:int}")]
        public async Task<ActionResult<ResponseDTO>> GetBooking([FromRoute] int bookingId)
        {
            return Ok(await _bookingService.GetBooking(bookingId));
        }
        [HttpPut("UpdateBookingStatus/{bookingId:int}")]
        public async Task<ActionResult<ResponseDTO>> UpdateBookingStatus([FromRoute] int bookingId, string status)
        {
            return Ok(await _bookingService.UpdateStatus(bookingId, status));
        }
    }
}
