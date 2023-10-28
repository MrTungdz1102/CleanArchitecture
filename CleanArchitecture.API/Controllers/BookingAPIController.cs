using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Entities;
using CleanArchitecture.ApplicationCore.Interfaces.Services;
using CleanArchitecture.ApplicationCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace CleanArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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

        [HttpGet("GetAllBookingUser")]
        public async Task<ActionResult<ResponseDTO>> GetAllBooking(string userId, string? status)
        {
            return Ok(await _bookingService.GetAllBookingUser(userId, status));
        }

        [HttpGet("GetAllBooking")]
        [Authorize(Roles ="ADMIN, MANAGER")]
        public async Task<ActionResult<ResponseDTO>> GetAllBooking(string? status)
        {
            return Ok(await _bookingService.GetAllBooking(status));
        }

        [HttpGet("GetBooking/{bookingId:int}")]
        public async Task<ActionResult<ResponseDTO>> GetBooking([FromRoute] int bookingId)
        {
            return Ok(await _bookingService.GetBooking(bookingId));
        }
        [HttpPut("UpdateBookingStatus/{bookingId:int}")]
        public async Task<ActionResult<ResponseDTO>> UpdateBookingStatus([FromRoute] int bookingId, string status, int villaNumber)
        {
            return Ok(await _bookingService.UpdateStatus(bookingId, status, villaNumber));
        }
    }
}
