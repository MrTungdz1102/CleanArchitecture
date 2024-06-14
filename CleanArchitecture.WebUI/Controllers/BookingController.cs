using CleanArchitecture.WebUI.Models;
using CleanArchitecture.WebUI.Models.DTOs;
using CleanArchitecture.WebUI.Models.ViewModel;
using CleanArchitecture.WebUI.Services.Interfaces;
using CleanArchitecture.WebUI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIORenderer;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using System.Security.Claims;

namespace CleanArchitecture.WebUI.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly IVillaService _villaService;
        private readonly IBookingService _bookingService;
        private readonly IPaymentService _paymentService;
        private readonly IVillaNumberService _villaNumberService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IEmailService _emailService;
        private readonly ICouponService _couponService;

        public BookingController(IVillaService villaService, IBookingService bookingService, IPaymentService paymentService, IVillaNumberService villaNumberService, IWebHostEnvironment webHostEnvironment, IEmailService emailService, ICouponService couponService)
        {
            _villaService = villaService;
            _bookingService = bookingService;
            _paymentService = paymentService;
            _villaNumberService = villaNumberService;
            _webHostEnvironment = webHostEnvironment;
            _emailService = emailService;
            _couponService = couponService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CheckIn(Booking booking)
        {
            ResponseDTO? response = await _bookingService.UpdateBookingStatus(booking.Id, Constants.StatusCheckedIn, booking.VillaNumber);
            if (response is not null && response.IsSuccess)
            {
                TempData["success"] = "Booking Updated Successfully.";
                return RedirectToAction(nameof(BookingDetails), new { bookingId = booking.Id });
            }
            else
            {
                TempData["error"] = response?.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> CheckOut(Booking booking)
        {
            ResponseDTO? response = await _bookingService.UpdateBookingStatus(booking.Id, Constants.StatusCompleted, booking.VillaNumber);
            if (response is not null && response.IsSuccess)
            {
                TempData["success"] = "Booking Updated Successfully.";
                return RedirectToAction(nameof(BookingDetails), new { bookingId = booking.Id });
            }
            else
            {
                TempData["error"] = response?.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> CancelBooking(Booking booking)
        {
            ResponseDTO? response = new();
            if (booking.Status == Constants.StatusPending)
            {
                response = await _bookingService.UpdateBookingStatus(booking.Id, Constants.StatusCancelled, 0);
            }

            if (booking.Status == Constants.StatusApproved)
            {
                response = await _paymentService.Refund(booking.StripePaymentIntentId!);
                response = await _bookingService.UpdateBookingStatus(booking.Id, Constants.StatusRefunded, 0);
            }
            if (response is not null && response.IsSuccess)
            {
                TempData["success"] = "Booking Canceled Successfully.";
                return RedirectToAction(nameof(BookingDetails), new { bookingId = booking.Id });
            }
            else
            {
                TempData["error"] = response?.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> BookingDetails(int bookingId)
        {
            ResponseDTO? response = await _bookingService.GetBooking(bookingId);
            Booking booking = new Booking();
            if (response is not null && response.IsSuccess)
            {
                booking = JsonConvert.DeserializeObject<Booking>(response.Result.ToString());
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(booking);
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
            DateTime dateTime = booking.CheckInDate.ToDateTime(TimeOnly.Parse("10:00 PM"));
            long date = dateTime.ToUnixTime();
            response = await _villaService.IsVillaAvailableByDate(villa.Id, booking.Nights, date);
            if (!response.IsSuccess)
            {
                {
                    TempData["error"] = "Room has been sold out!";
                    //no rooms available
                    return RedirectToAction(nameof(FinalizeBooking), new
                    {
                        villaId = booking.VillaId,
                        checkInDate = booking.CheckInDate,
                        nights = booking.Nights
                    });
                }
            }
            if (booking.CouponCode is not null)
            {
                Coupon? coupon = null;
                response = await _couponService.GetCouponByCode(booking.CouponCode);
                if (response is not null && response.IsSuccess)
                {
                    coupon = JsonConvert.DeserializeObject<Coupon>(Convert.ToString(response.Result));
                    booking.TotalCost = villa.Price * booking.Nights - coupon.DiscountAmount;
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            else
            {
                booking.TotalCost = villa.Price * booking.Nights;
            }

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
                    await _bookingService.UpdateBookingStatus(bookingId, Constants.StatusApproved, 0);
                    await _bookingService.UpdateBookingPayment(bookingId, paymentResponse.StripeSessionId, paymentResponse.PaymentIntentId);

                    string? filePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "booking_success.html");

                    string content = await System.IO.File.ReadAllTextAsync(filePath);

                    content = content
                .Replace("{{UserName}}", HttpContext.User.FindFirstValue(ClaimTypes.Name))
                .Replace("{{BookingId}}", bookingId.ToString())
                .Replace("{{Participants}}", booking.Participants.ToString())
                .Replace("{{HotelName}}", booking.Villa.Name)
                .Replace("{{TotalPrice}}", booking.TotalCost.ToString())
                .Replace("{{CheckInDate}}", booking.CheckInDate.ToString())
                .Replace("{{Discount}}", (booking.Villa.Price * booking.Nights - booking.TotalCost).ToString())
                .Replace("{{PaymentDate}}", booking.PaymentDate.ToString())
                .Replace("{{CheckOutDate}}", booking.CheckOutDate.ToString());

                    EmailVM email = new EmailVM
                    {
                        Email = HttpContext.User.FindFirstValue(ClaimTypes.Email),
                        Subject = "Booking Confirmation",
                        Message = content
                    };
                    await _emailService.SendEmailAsync(email);
                    TempData["succcess"] = "Your order has been successfully paid. Please check the details in your email.";
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            return View(bookingId);
        }

        public async Task<IActionResult> ApplyCoupon(string couponCode)
        {
            ResponseDTO? response = await _couponService.GetCouponByCode(couponCode);
            Coupon? coupon = null;
            if (response.Result is not null && response.IsSuccess)
            {
                coupon = JsonConvert.DeserializeObject<Coupon>(response.Result.ToString());
            }
            return Json(coupon);
        }

        [HttpPost]
        public async Task<IActionResult> GenerateInvoice(int id, string downloadType)
        {
            string basePath = _webHostEnvironment.WebRootPath;
            WordDocument document = new WordDocument();
            string dataPath = basePath + "/exports/BookingDetails.docx";
            // load template file
            using FileStream fileStream = new FileStream(dataPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            document.Open(fileStream, formatType: Syncfusion.DocIO.FormatType.Automatic);
            // update data for template
            ResponseDTO? response = await _bookingService.GetBooking(id);
            Booking? booking = new Booking();
            if (response.IsSuccess && response.Result != null)
            {
                booking = JsonConvert.DeserializeObject<Booking>(response.Result.ToString());
            }
            else
            {
                TempData["error"] = response?.Message;
                return RedirectToAction(nameof(Index));
            }
            TextSelection textSelection = document.Find("xx_customer_name", false, true);
            WTextRange textRange = textSelection.GetAsOneRange();
            textRange.Text = booking.Name;
            textSelection = document.Find("xx_customer_phone", false, true);
            textRange = textSelection.GetAsOneRange();
            textRange.Text = booking.Phone;

            textSelection = document.Find("xx_customer_email", false, true);
            textRange = textSelection.GetAsOneRange();
            textRange.Text = booking.Email;

            textSelection = document.Find("XX_BOOKING_NUMBER", false, true);
            textRange = textSelection.GetAsOneRange();
            textRange.Text = "BOOKING ID - " + booking.Id;
            textSelection = document.Find("XX_BOOKING_DATE", false, true);
            textRange = textSelection.GetAsOneRange();
            textRange.Text = "BOOKING DATE - " + booking.BookingDate.ToShortDateString();


            textSelection = document.Find("xx_payment_date", false, true);
            textRange = textSelection.GetAsOneRange();
            textRange.Text = booking.PaymentDate.ToShortDateString();
            textSelection = document.Find("xx_checkin_date", false, true);
            textRange = textSelection.GetAsOneRange();
            textRange.Text = booking.CheckInDate.ToShortDateString();
            textSelection = document.Find("xx_checkout_date", false, true);
            textRange = textSelection.GetAsOneRange();
            textRange.Text = booking.CheckOutDate.ToShortDateString(); ;
            textSelection = document.Find("xx_booking_total", false, true);
            textRange = textSelection.GetAsOneRange();
            textRange.Text = booking.TotalCost.ToString("c");

            WTable table = new(document);

            table.TableFormat.Borders.LineWidth = 1f;
            table.TableFormat.Borders.Color = Color.Black;
            table.TableFormat.Paddings.Top = 7f;
            table.TableFormat.Paddings.Bottom = 7f;
            table.TableFormat.Borders.Horizontal.LineWidth = 1f;

            int rows = booking.VillaNumber > 0 ? 3 : 2;
            table.ResetCells(rows, 4);

            WTableRow row0 = table.Rows[0];

            row0.Cells[0].AddParagraph().AppendText("NIGHTS");
            row0.Cells[0].Width = 80;
            row0.Cells[1].AddParagraph().AppendText("VILLA");
            row0.Cells[1].Width = 220;
            row0.Cells[2].AddParagraph().AppendText("PRICE PER NIGHT");
            row0.Cells[3].AddParagraph().AppendText("TOTAL");
            row0.Cells[3].Width = 80;

            WTableRow row1 = table.Rows[1];

            row1.Cells[0].AddParagraph().AppendText(booking.Nights.ToString());
            row1.Cells[0].Width = 80;
            row1.Cells[1].AddParagraph().AppendText(booking.Villa.Name);
            row1.Cells[1].Width = 220;
            row1.Cells[2].AddParagraph().AppendText((booking.TotalCost / booking.Nights).ToString("c"));
            row1.Cells[3].AddParagraph().AppendText(booking.TotalCost.ToString("c"));
            row1.Cells[3].Width = 80;

            if (booking.VillaNumber > 0)
            {
                WTableRow row2 = table.Rows[2];

                row2.Cells[0].Width = 80;
                row2.Cells[1].AddParagraph().AppendText("Villa Number - " + booking.VillaNumber.ToString());
                row2.Cells[1].Width = 220;
                row2.Cells[3].Width = 80;
            }

            WTableStyle tableStyle = document.AddTableStyle("CustomStyle") as WTableStyle;
            tableStyle.TableProperties.RowStripe = 1;
            tableStyle.TableProperties.ColumnStripe = 2;
            tableStyle.TableProperties.Paddings.Top = 2;
            tableStyle.TableProperties.Paddings.Bottom = 1;
            tableStyle.TableProperties.Paddings.Left = 5.4f;
            tableStyle.TableProperties.Paddings.Right = 5.4f;

            ConditionalFormattingStyle firstRowStyle = tableStyle.ConditionalFormattingStyles.Add(ConditionalFormattingType.FirstRow);
            firstRowStyle.CharacterFormat.Bold = true;
            firstRowStyle.CharacterFormat.TextColor = Color.FromArgb(255, 255, 255, 255);
            firstRowStyle.CellProperties.BackColor = Color.Black;

            table.ApplyStyle("CustomStyle");

            TextBodyPart bodyPart = new(document);
            bodyPart.BodyItems.Add(table);

            document.Replace("<ADDTABLEHERE>", bodyPart, false, false);
            using DocIORenderer renderer = new();
            MemoryStream stream = new();
            if (downloadType == "word")
            {

                document.Save(stream, FormatType.Docx);
                stream.Position = 0;

                return File(stream, "application/docx", "BookingDetails.docx");
            }
            else
            {
                PdfDocument pdfDocument = renderer.ConvertToPDF(document);
                pdfDocument.Save(stream);
                stream.Position = 0;

                return File(stream, "application/pdf", "BookingDetails.pdf");
            }
        }

        #region api call
        public async Task<IActionResult> GetAll(string? status = null)
        {
            string? userId = null;
            if (string.IsNullOrEmpty(status))
            {
                status = "";
            }
            if (User.IsInRole(Constants.Role_Admin))
            {
                ResponseDTO? response = await _bookingService.GetAllBooking(status);
                if (response.Result is not null && response.IsSuccess)
                {
                    List<Booking> result = JsonConvert.DeserializeObject<List<Booking>>(response.Result.ToString());
                    return Json(new { data = result });
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            else if (User.IsInRole(Constants.Role_Customer))
            {
                userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                ResponseDTO? response = await _bookingService.GetAllBookingUser(userId, status, true);
                if (response.Result is not null && response.IsSuccess)
                {
                    List<Booking> result = JsonConvert.DeserializeObject<List<Booking>>(response.Result.ToString());
                    return Json(new { data = result });
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            else
            {
                userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                ResponseDTO? response = await _bookingService.GetAllBookingUser(userId, status, false);
                if (response.Result is not null && response.IsSuccess)
                {
                    List<Booking> result = JsonConvert.DeserializeObject<List<Booking>>(response.Result.ToString());
                    return Json(new { data = result });
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
