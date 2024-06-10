using CleanArchitecture.WebUI.Models;
using CleanArchitecture.WebUI.Models.DTOs;
using CleanArchitecture.WebUI.Models.ViewModel;
using CleanArchitecture.WebUI.Services.Interfaces;
using CleanArchitecture.WebUI.Utilities;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Syncfusion.Presentation;
using System.Diagnostics;
using System.Security.Claims;

namespace CleanArchitecture.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IVillaService _villaService;
        private readonly IAmenityService _amenityService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ICityService _cityService;
        private readonly IReviewService _reviewService;

        public HomeController(ILogger<HomeController> logger, IVillaService villaService, IAmenityService amenityService, IWebHostEnvironment webHostEnvironment, ICityService cityService, IReviewService reviewService)
        {
            _logger = logger;
            _villaService = villaService;
            _amenityService = amenityService;
            _webHostEnvironment = webHostEnvironment;
            _cityService = cityService;
            _reviewService = reviewService;
        }
        //  [Authorize]
        public async Task<IActionResult> Index()
        {
            //  var culture = CultureInfo.CurrentCulture.Name;
            int nights = 1;
            long date = DateTime.Now.ToUnixTime();
            ResponseDTO? response = await _villaService.GetAllDetailVilla(nights, date, null, null, null, null);
            List<Villa> villas = new List<Villa>();
            List<City> cities = new List<City>();
            if (response.Result != null && response.IsSuccess)
            {
                villas = JsonConvert.DeserializeObject<List<Villa>>(Convert.ToString(response.Result));
                response = await _cityService.GetAllCity();
                if (response.Result != null && response.IsSuccess)
                {
                    cities = JsonConvert.DeserializeObject<List<City>>(Convert.ToString(response.Result));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            foreach (var vila in villas)
            {
                response = await _reviewService.GetAllReviewByVillaId(vila.Id);
                if (response.Result != null && response.IsSuccess)
                {
                    vila.ReviewList = JsonConvert.DeserializeObject<List<Review>>(Convert.ToString(response.Result));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }

            }
            HomeVM homeVM = new HomeVM
            {
                VillaList = villas,
                Nights = 1,
                CityList = cities,
                SelectCity = cities.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                SelectPrice = new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text = "50$",
                        Value = "50"
                    },
                     new SelectListItem
                    {
                        Text = "100$",
                        Value = "100"
                    },
                     new SelectListItem
                    {
                        Text = "200$",
                        Value = "200"
                    },
                      new SelectListItem
                    {
                        Text = "300$",
                        Value = "300"
                    },
                       new SelectListItem
                    {
                        Text = "500$",
                        Value = "500"
                    }, new SelectListItem
                    {
                        Text = "1000$",
                        Value = "1000"
                    }
                }
            };
            homeVM.CheckInDate = DateOnly.FromDateTime(DateTime.Now);
            return View(homeVM);
        }

        [HttpPost]
        public async Task<IActionResult> GetVillaByDate(int nights, DateOnly checkInDate, string? keyword, int? cityId, double? priceFrom, double? priceTo)
        {
            Thread.Sleep(250);
            DateTime dateTime = checkInDate.ToDateTime(TimeOnly.Parse("10:00 PM"));
            long date = dateTime.ToUnixTime();
            ResponseDTO? response = await _villaService.GetAllDetailVilla(nights, date, keyword, cityId, priceFrom, priceTo);
            List<Villa> villas = new List<Villa>();
            if (response.Result != null && response.IsSuccess)
            {
                villas = JsonConvert.DeserializeObject<List<Villa>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            foreach (var vila in villas)
            {
                response = await _reviewService.GetAllReviewByVillaId(vila.Id);
                if (response.Result != null && response.IsSuccess)
                {
                    vila.ReviewList = JsonConvert.DeserializeObject<List<Review>>(Convert.ToString(response.Result));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }

            }
            HomeVM homeVM = new HomeVM()
            {
                VillaList = villas,
                CheckInDate = checkInDate,
                Nights = nights
            };
            return PartialView("_VillaListPartial", homeVM);
        }

        [HttpPost]
        public async Task<IActionResult> GeneratePPTExport(int id)
        {
            Villa? villa = null;
            ResponseDTO? response = await _villaService.GetVillaById(id);
            if (response != null && response.IsSuccess)
            {
                villa = JsonConvert.DeserializeObject<Villa>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
                return RedirectToAction(nameof(Index));
            }
            string basePath = _webHostEnvironment.WebRootPath;
            string filePath = basePath + @"/Exports/ExportVillaDetails.pptx";
            using IPresentation presentation = Presentation.Open(filePath);
            ISlide slide = presentation.Slides[0];


            IShape? shape = slide.Shapes.FirstOrDefault(u => u.ShapeName == "txtVillaName") as IShape;
            if (shape is not null)
            {
                shape.TextBody.Text = villa.Name;
            }

            shape = slide.Shapes.FirstOrDefault(u => u.ShapeName == "txtVillaDescription") as IShape;
            if (shape is not null)
            {
                shape.TextBody.Text = villa.Description;
            }


            shape = slide.Shapes.FirstOrDefault(u => u.ShapeName == "txtOccupancy") as IShape;
            if (shape is not null)
            {
                shape.TextBody.Text = string.Format("Max Occupancy : {0} adults", villa.Occupancy);
            }
            shape = slide.Shapes.FirstOrDefault(u => u.ShapeName == "txtVillaSize") as IShape;
            if (shape is not null)
            {
                shape.TextBody.Text = string.Format("Villa Size: {0} sqft", villa.SquareFeet);
            }
            shape = slide.Shapes.FirstOrDefault(u => u.ShapeName == "txtPricePerNight") as IShape;
            if (shape is not null)
            {
                shape.TextBody.Text = string.Format("USD {0}/night", villa.Price.ToString("C"));
            }


            shape = slide.Shapes.FirstOrDefault(u => u.ShapeName == "txtVillaAmenitiesHeading") as IShape;
            if (shape is not null)
            {
                List<string> listItems = villa.VillaAmenity.Select(x => x.Name).ToList();

                shape.TextBody.Text = "";

                foreach (var item in listItems)
                {
                    IParagraph paragraph = shape.TextBody.AddParagraph();
                    ITextPart textPart = paragraph.AddTextPart(item);

                    paragraph.ListFormat.Type = ListType.Bulleted;
                    paragraph.ListFormat.BulletCharacter = '\u2022';
                    textPart.Font.FontName = "system-ui";
                    textPart.Font.FontSize = 18;
                    textPart.Font.Color = ColorObject.FromArgb(144, 148, 152);
                }
            }

            shape = slide.Shapes.FirstOrDefault(u => u.ShapeName == "imgVilla") as IShape;
            if (shape is not null)
            {
                byte[] imageData;
                string imageUrl;
                try
                {
                    //  imageUrl = string.Format("{0}{1}", basePath, villa.ImageUrl);
                    imageUrl = villa.ImageLocalPath;
                    imageData = System.IO.File.ReadAllBytes(imageUrl);
                }
                catch (Exception)
                {
                    imageUrl = string.Format("{0}{1}", basePath, "/images/placeholder.png");
                    imageData = System.IO.File.ReadAllBytes(imageUrl);
                }
                slide.Shapes.Remove(shape);
                using MemoryStream imageStream = new(imageData);
                IPicture newPicture = slide.Pictures.AddPicture(imageStream, 60, 120, 300, 200);

            }

            MemoryStream memoryStream = new();
            presentation.Save(memoryStream);
            memoryStream.Position = 0;
            return File(memoryStream, "application/pptx", "villa.pptx");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult OnGetSetCultureCookie(string cltr, string returnUrl)
        {
            Response.Cookies.Append(
        CookieRequestCultureProvider.DefaultCookieName,
        CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(cltr)),
        new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
            return LocalRedirect(returnUrl);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview(string? content, int? rating, int villaId)
        {
            Review review = new Review();
            review.CreatedAt = DateTime.Now;
            review.ReviewContent = content;
            review.Rating = rating;
            review.VillaId = villaId;
            review.UserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            review.UserName = HttpContext.User.FindFirstValue(ClaimTypes.Name);

            ResponseDTO? response = await _reviewService.CreateReview(review);
            if (response is not null && response.IsSuccess)
            {
                TempData["success"] = "Revie successfully";
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateReview(int reviewId, string? content, int? rating, int villaId)
        {
            ResponseDTO? response = await _reviewService.GetReview(reviewId);
            if (response is not null && response.IsSuccess)
            {
                Review review = new Review();
                review.ModifiedAt = DateTime.Now;
                review.ReviewContent = content;
                review.Rating = rating;
                review.VillaId = villaId;
                review.UserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                review.UserName = HttpContext.User.FindFirstValue(ClaimTypes.Name);
                response = await _reviewService.UpdateReview(review);
                if (response is not null && response.IsSuccess)
                {
                    TempData["success"] = "Review updated successfully";
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            ResponseDTO? response = await _reviewService.DeleteReview(reviewId);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Review deleted successfully";
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}