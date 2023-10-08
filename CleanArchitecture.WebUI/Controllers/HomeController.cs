using CleanArchitecture.WebUI.Models;
using CleanArchitecture.WebUI.Models.DTOs;
using CleanArchitecture.WebUI.Models.ViewModel;
using CleanArchitecture.WebUI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace CleanArchitecture.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IVillaService _villaService;
        private readonly IAmenityService _amenityService;
        public HomeController(ILogger<HomeController> logger, IVillaService villaService, IAmenityService amenityService)
        {
            _logger = logger;
            _villaService = villaService;
            _amenityService = amenityService;
        }
      //  [Authorize]
        public async Task<IActionResult> Index()
        {
            ResponseDTO? response = await _villaService.GetAllDetailVilla();
            List<Villa> villas = new List<Villa>();
            if(response.Result != null && response.IsSuccess)
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
                Nights = 1,
                CheckInDate = DateOnly.FromDateTime(DateTime.Now)
            };
            return View(homeVM);
        }

        [HttpPost]
        public async Task<IActionResult> GetVillaByDate(int nights, DateOnly checkInDate)
        {
            HomeVM homeVM = new HomeVM()
            {
                CheckInDate = checkInDate,
                Nights = nights
            };
            return PartialView("_VillaListPartial", homeVM);
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