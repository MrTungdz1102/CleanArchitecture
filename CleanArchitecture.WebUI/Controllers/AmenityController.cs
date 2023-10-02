using CleanArchitecture.WebUI.Models;
using CleanArchitecture.WebUI.Models.DTOs;
using CleanArchitecture.WebUI.Services.Interfaces;
using CleanArchitecture.WebUI.Utilities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CleanArchitecture.WebUI.Controllers
{
    public class AmenityController : Controller
    {
        private readonly IAmenityService _aminityService;

        public AmenityController(IAmenityService amenityService)
        {
            _aminityService = amenityService;
        }
        public async Task<IActionResult> Index(int? page)
        {
            QueryParameter query = new QueryParameter
            {
                PageSize = 7,
                PageNumber = (page == null || page < 0) ? 1 : page.Value
            };
            ViewBag.PageNumber = page;
            ResponseDTO? response = await _aminityService.GetAllAmenity(query);
            List<Amenity> amenityList = null;
            PageResult<Amenity> pageResult = new PageResult<Amenity>();
            if (response != null && response.IsSuccess)
            {
                pageResult = JsonConvert.DeserializeObject<PageResult<Amenity>>(Convert.ToString(response.Result));
                amenityList = pageResult.Items;
                ViewBag.TotalCount = (int)Math.Ceiling((double)pageResult.TotalCount / query.PageSize);
            }
            else
            {
                TempData["error"] = response?.Message;
                return RedirectToAction("Index", "Home");
            }
            return View(amenityList);
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Update(int amenityId)
        {
            return View();
        }
        public IActionResult Delete(int amenityId)
        {
            return View();
        }
    }
}
