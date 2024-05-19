using CleanArchitecture.WebUI.Models;
using CleanArchitecture.WebUI.Models.DTOs;
using CleanArchitecture.WebUI.Models.ViewModel;
using CleanArchitecture.WebUI.Services.Interfaces;
using CleanArchitecture.WebUI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Security.Claims;

namespace CleanArchitecture.WebUI.Controllers
{
    [Authorize(Roles = Constants.Role_Admin)]
    public class VillaController : Controller
    {
        private readonly IVillaService _villaService;
        private readonly ICityService _cityService;

        public VillaController(IVillaService villaService, ICityService cityService)
        {
            _villaService = villaService;
            _cityService = cityService;
        }
        public async Task<IActionResult> Index()
        {
            List<Villa> villaList = new List<Villa>();
            string? userId = null;
            if (User.IsInRole(Constants.Role_Customer))
            {
                userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            ResponseDTO? response = await _villaService.GetAllVilla(userId);
            if (response != null && response.IsSuccess)
            {
                villaList = JsonConvert.DeserializeObject<List<Villa>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
                return RedirectToAction("Index", "Home");
            }
            return View(villaList);
        }
        public async Task<IActionResult> Create()
        {
            ResponseDTO? response = await _cityService.GetAllCity();
            List<City> cityList = new List<City>();
            if (response != null && response.IsSuccess)
            {
                cityList = JsonConvert.DeserializeObject<List<City>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
                return RedirectToAction("Index", "Home");
            }
            VillaVM villaVM = new VillaVM
            {
                CityList = cityList.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };
            return View(villaVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(VillaVM villaVM)
        {
            if (User.IsInRole(Constants.Role_Customer))
            {
                villaVM.Villa.OwnerId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            ResponseDTO? response = await _villaService.CreateVilla(villaVM.Villa);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Villa created successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = response?.Message;
                response = await _cityService.GetAllCity();
                List<City> cityList = new List<City>();
                if (response != null && response.IsSuccess)
                {
                    cityList = JsonConvert.DeserializeObject<List<City>>(Convert.ToString(response.Result));
                }
                else
                {
                    TempData["error"] = response?.Message;
                    return RedirectToAction("Index", "Home");
                }
                villaVM = new VillaVM
                {
                    CityList = cityList.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    })
                };
            }
            return View(villaVM);
        }

        public async Task<IActionResult> Update(int villaId)
        {
            Villa? villa = null;
            IEnumerable<City> listCity = new List<City>();
            ResponseDTO? response1 = await _villaService.GetVillaById(villaId);
            ResponseDTO? response2 = await _cityService.GetAllCity();
            if ((response1 != null && response1.IsSuccess) && (response2 != null && response2.IsSuccess))
            {
                villa = JsonConvert.DeserializeObject<Villa>(Convert.ToString(response1.Result));
                listCity = JsonConvert.DeserializeObject<List<City>>(Convert.ToString(response2.Result));
            }
            else
            {
                TempData["error"] = response1?.Message;
                TempData["error"] = response2?.Message;
            }
            VillaVM villaVM = new VillaVM
            {
                CityList = listCity.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                Villa = villa
            };
            return View(villaVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(VillaVM villaVM)
        {
            if (User.IsInRole(Constants.Role_Customer))
            {
                villaVM.Villa.OwnerId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            ResponseDTO? response = await _villaService.UpdateVilla(villaVM.Villa);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Villa updated successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = response?.Message;
                response = await _cityService.GetAllCity();
                List<City> cityList = new List<City>();
                if (response != null && response.IsSuccess)
                {
                    cityList = JsonConvert.DeserializeObject<List<City>>(Convert.ToString(response.Result));
                }
                else
                {
                    TempData["error"] = response?.Message;
                    return RedirectToAction("Index", "Home");
                }
                villaVM = new VillaVM
                {
                    CityList = cityList.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    })
                };
            }
            return View(villaVM);
        }
        public async Task<IActionResult> Delete(int villaId)
        {
            Villa? villa = null;
            IEnumerable<City> listCity = new List<City>();
            ResponseDTO? response1 = await _villaService.GetVillaById(villaId);
            ResponseDTO? response2 = await _cityService.GetAllCity();
            if ((response1 != null && response1.IsSuccess) && (response2 != null && response2.IsSuccess))
            {
                villa = JsonConvert.DeserializeObject<Villa>(Convert.ToString(response1.Result));
                listCity = JsonConvert.DeserializeObject<List<City>>(Convert.ToString(response2.Result));
            }
            else
            {
                TempData["error"] = response1?.Message;
            }
            VillaVM villaVM = new VillaVM
            {
                CityList = listCity.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                Villa = villa
            };
            return View(villaVM);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(VillaVM villaVM)
        {
            ResponseDTO? response = await _villaService.DeleteVilla(villaVM.Villa.Id);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Villa deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(villaVM);
        }
    }
}
