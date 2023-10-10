using CleanArchitecture.WebUI.Models;
using CleanArchitecture.WebUI.Models.DTOs;
using CleanArchitecture.WebUI.Services.Interfaces;
using CleanArchitecture.WebUI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace CleanArchitecture.WebUI.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly IVillaService _villaService;
        private readonly IBookingService _bookingService;
        public BookingController(IVillaService villaService, IBookingService bookingService)
        {
            _villaService = villaService;
            _bookingService = bookingService;

        }
        public async Task<IActionResult> FinalizeBooking(int villaId, DateOnly checkInDate, int nights)
        {
            var phoneNumber = HttpContext.User.FindFirstValue(ClaimTypes.MobilePhone);
            var name = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var email = HttpContext.User.FindFirstValue(ClaimTypes.Email);
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            ResponseDTO response = await _villaService.GetVillaById(villaId);
            Villa villa = new Villa();
            if(response.Result != null && response.IsSuccess)
            {
                villa = JsonConvert.DeserializeObject<Villa>(response.Result.ToString());
            }
            else
            {
                TempData["error"] = response?.Message;
                return RedirectToAction("Index", "Home");
            }
            Booking booking = new Booking
            {
                VillaId = villaId,
                Villa = villa,
                CheckInDate = checkInDate,
                Nights = nights,
                CheckOutDate = checkInDate.AddDays(nights),
                Phone = phoneNumber,
                Email = email,
                Name = name,
                UserId = userId
            };
            booking.TotalCost = booking.Villa.Price * nights;
            return View(booking);
        }
        [HttpPost]
        public async Task<IActionResult> FinalizeBooking(Booking booking)
        {
            ResponseDTO? response = await _villaService.GetVillaById(booking.VillaId);
            Villa villa = new Villa();
            if (response.Result != null && response.IsSuccess)
            {
                villa = JsonConvert.DeserializeObject<Villa>(response.Result.ToString());
            }
            else
            {
                TempData["error"] = response?.Message;
                return RedirectToAction("Index", "Home");
            }
            booking.TotalCost = villa.Price * booking.Nights;
            booking.Status = Constants.StatusPending;
            booking.BookingDate = DateTime.Now;
            ResponseDTO? response1 = await _bookingService.CreateBooking(booking);
            if (response1.Result != null && response1.IsSuccess)
            {
                TempData["success"] = "Booking Successfully!";
                return RedirectToAction(nameof(BookingConfirmation), new { bookingId = booking.Id });
            }
            else
            {
                TempData["error"] = response1?.Message;
            }
            return RedirectToAction(nameof(FinalizeBooking));
        }

        public IActionResult BookingConfirmation(int bookingId)
        {
            return View(bookingId);
        }
    }
}
