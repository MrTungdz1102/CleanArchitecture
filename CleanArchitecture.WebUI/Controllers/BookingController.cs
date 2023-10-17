using CleanArchitecture.WebUI.Models;
using CleanArchitecture.WebUI.Models.DTOs;
using CleanArchitecture.WebUI.Models.ViewModel;
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
        private readonly IPaymentService _paymentService;
        public BookingController(IVillaService villaService, IBookingService bookingService, IPaymentService paymentService)
        {
            _villaService = villaService;
            _bookingService = bookingService;
            _paymentService = paymentService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult BookingDetails()
        {
            return View();
        }

        public async Task<IActionResult> FinalizeBooking(int villaId, DateOnly checkInDate, int nights)
        {
            var phoneNumber = HttpContext.User.FindFirstValue(ClaimTypes.MobilePhone);
            var name = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var email = HttpContext.User.FindFirstValue(ClaimTypes.Email);
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            ResponseDTO response = await _villaService.GetVillaById(villaId);
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
                Booking bookingResult = JsonConvert.DeserializeObject<Booking>(response1.Result.ToString());
                var domain = Request.Scheme + "://" + Request.Host.Value + "/";
                StripePaymentRequest stripePayment = new StripePaymentRequest
                {
                    ApprovedUrl = domain + "Booking/BookingConfirmation?bookingId=" + bookingResult.Id,
                    CancelUrl = domain + $"Booking/FinalizeBooking?villaId={booking.VillaId}&checkInDate={booking.CheckInDate}&nights={booking.Nights}",
                    Price = booking.TotalCost,
                    Name = villa.Name
                };
                response = await _paymentService.CreateCheckout(stripePayment);
                if (response != null && response.IsSuccess)
                {
                    PaymentResponse paymentResponse = JsonConvert.DeserializeObject<PaymentResponse>(response.Result.ToString());
                    response = await _bookingService.UpdateBookingPayment(bookingResult.Id, paymentResponse.StripeSessionId, "0");
                    Response.Headers.Add("Location", paymentResponse.StripeSessionUrl);
                    return new StatusCodeResult(303);
                }
            }
            else
            {
                TempData["error"] = response1?.Message;
            }
            return RedirectToAction(nameof(FinalizeBooking));
        }

        public async Task<IActionResult> BookingConfirmation(int bookingId)
        {
            ResponseDTO? response = await _bookingService.GetBooking(bookingId);
            Booking? booking = new Booking();
            if (response.IsSuccess && response.Result != null)
            {
                booking = JsonConvert.DeserializeObject<Booking>(response.Result.ToString());
                response = await _paymentService.ValidatePayment(booking.StripeSessionId);
                if (response.IsSuccess && response.Result != null)
                {
                    PaymentResponse paymentResponse = JsonConvert.DeserializeObject<PaymentResponse>(response.Result.ToString());
                    await _bookingService.UpdateBookingStatus(bookingId, Constants.StatusApproved);
                    await _bookingService.UpdateBookingPayment(bookingId, paymentResponse.StripeSessionId, paymentResponse.PaymentIntentId);
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            return View(bookingId);
        }
    }
}
