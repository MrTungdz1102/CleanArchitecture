using CleanArchitecture.WebUI.Models;
using CleanArchitecture.WebUI.Models.DTOs;
using CleanArchitecture.WebUI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CleanArchitecture.WebUI.Controllers
{
    public class BookingController : Controller
    {
        private readonly IVillaService _villaService;
        public BookingController(IVillaService villaService)
        {
            _villaService = villaService;
        }
        public async Task<IActionResult> FinalizeBooking(int villaId, DateOnly checkInDate, int nights)
        {
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
                CheckOutDate = checkInDate.AddDays(nights)
            };
            booking.TotalCost = booking.Villa.Price * nights;
            return View(booking);
        }
    }
}
