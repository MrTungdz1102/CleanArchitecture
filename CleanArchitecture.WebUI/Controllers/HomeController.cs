using CleanArchitecture.WebUI.Models;
using CleanArchitecture.WebUI.Models.DTOs;
using CleanArchitecture.WebUI.Models.ViewModel;
using CleanArchitecture.WebUI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Syncfusion.Presentation;
using System.Diagnostics;

namespace CleanArchitecture.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IVillaService _villaService;
        private readonly IAmenityService _amenityService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public HomeController(ILogger<HomeController> logger, IVillaService villaService, IAmenityService amenityService, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _villaService = villaService;
            _amenityService = amenityService;
            _webHostEnvironment = webHostEnvironment;
        }
        //  [Authorize]
        public async Task<IActionResult> Index()
        {
            int nights = 1;
            DateOnly date = DateOnly.FromDateTime(DateTime.Now);
            ResponseDTO? response = await _villaService.GetAllDetailVilla(nights, date);
            List<Villa> villas = new List<Villa>();
            if (response.Result != null && response.IsSuccess)
            {
                villas = JsonConvert.DeserializeObject<List<Villa>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            HomeVM homeVM = new HomeVM
            {
                VillaList = villas,
                Nights = 1
            };
            homeVM.CheckInDate = DateOnly.FromDateTime(DateTime.Now);
            return View(homeVM);
        }

        [HttpPost]
        public async Task<IActionResult> GetVillaByDate(int nights, DateOnly checkInDate)
        {
            Thread.Sleep(250);
            ResponseDTO? response = await _villaService.GetAllDetailVilla(nights, checkInDate);
            List<Villa> villas = new List<Villa>();
            if (response.Result != null && response.IsSuccess)
            {
                villas = JsonConvert.DeserializeObject<List<Villa>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
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
    }
}