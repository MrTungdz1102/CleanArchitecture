using CleanArchitecture.WebUI.Models;
using CleanArchitecture.WebUI.Models.DTOs;
using CleanArchitecture.WebUI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace CleanArchitecture.WebUI.Controllers
{
    public class CityController : Controller
    {
        private readonly ICityService _cityService;
        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }
        public async Task<IActionResult> Index()
        {
            List<City> cityList = new List<City>();
            ResponseDTO? response = await _cityService.GetAllCity();
            if (response != null && response.IsSuccess)
            {
                cityList = JsonConvert.DeserializeObject<List<City>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
                return RedirectToAction("Index", "Home");
            }
            return View(cityList);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(City city)
        {
            ResponseDTO? response = await _cityService.CreateCity(city);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "City created successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(city);
        }

        public async Task<IActionResult> Update(int id)
        {
            City? city = null;
            ResponseDTO? response = await _cityService.GetCity(id);
            if (response != null && response.IsSuccess)
            {
                city = JsonConvert.DeserializeObject<City>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(city);
        }
        [HttpPost]
        public async Task<IActionResult> Update(City city)
        {
            ResponseDTO? response = await _cityService.UpdateCity(city);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "City updated successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(city);
        }
        public async Task<IActionResult> Delete(int id)
        {
            City? city = null;
            ResponseDTO? response = await _cityService.GetCity(id);
            if (response != null && response.IsSuccess)
            {
                city = JsonConvert.DeserializeObject<City>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(city);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(City city)
        {
            ResponseDTO? response = await _cityService.DeleteCity(city.Id);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "City deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(city);
        }
    }
}
