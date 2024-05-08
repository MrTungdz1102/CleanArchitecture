using CleanArchitecture.WebUI.Models;
using CleanArchitecture.WebUI.Models.DTOs;
using CleanArchitecture.WebUI.Models.ViewModel;
using CleanArchitecture.WebUI.Services.Interfaces;
using CleanArchitecture.WebUI.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Security.Claims;

namespace CleanArchitecture.WebUI.Controllers
{
    public class AmenityController : Controller
    {
        private readonly IAmenityService _aminityService;
        private readonly IVillaService _villaService;
        public AmenityController(IAmenityService amenityService, IVillaService villaService)
        {
            _aminityService = amenityService;
            _villaService = villaService;

        }
        public async Task<IActionResult> Index(int? page)
        {
            QueryParameter query = new QueryParameter
            {
                PageSize = 7,
                PageNumber = (page == null || page < 0) ? 1 : page.Value
            };
            ViewBag.PageNumber = page ?? 0;
            string? userId = null;
            if (User.IsInRole(Constants.Role_Customer))
            {
                userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            ResponseDTO? response = await _aminityService.GetAllAmenity(query, userId);
            List<Amenity> amenityList = null;
            PageResult<Amenity>? pageResult = new PageResult<Amenity>();
            if (response!.Result != null && response.IsSuccess)
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
        public async Task<IActionResult> Create()
        {
            string? userId = null;
            if (User.IsInRole(Constants.Role_Customer))
            {
                userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            ResponseDTO? response = await _villaService.GetAllVilla(userId);
            List<Villa> villas = null;
            if (response.Result != null && response.IsSuccess)
            {
                villas = JsonConvert.DeserializeObject<List<Villa>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            AmenityVM amenityVM = new AmenityVM
            {
                VillaList = villas.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };
            return View(amenityVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(AmenityVM amenityVM)
        {
            ResponseDTO? response = await _aminityService.CreateAmenity(amenityVM.Amenity);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Amenity created successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = response?.Message;
                string? userId = null;
                if (User.IsInRole(Constants.Role_Customer))
                {
                    userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                }
                response = await _villaService.GetAllVilla(userId);
                IEnumerable<Villa> listVillaNumber = new List<Villa>();
                if (response != null && response.IsSuccess)
                {
                    listVillaNumber = JsonConvert.DeserializeObject<IEnumerable<Villa>>(Convert.ToString(response.Result));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
                amenityVM.VillaList = listVillaNumber.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });
            }
            return View(amenityVM);
        }

        public async Task<IActionResult> Update(int amenityId)
        {
            ResponseDTO? response = await _aminityService.GetAmenityById(amenityId);
            string? userId = null;
            if (User.IsInRole(Constants.Role_Customer))
            {
                userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            ResponseDTO? response1 = await _villaService.GetAllVilla(userId);
            Amenity amenity = null;
            List<Villa> villas = null;
            if (response != null && response.IsSuccess && response1 != null && response1.IsSuccess)
            {
                amenity = JsonConvert.DeserializeObject<Amenity>(Convert.ToString(response.Result));
                villas = JsonConvert.DeserializeObject<List<Villa>>(Convert.ToString(response1.Result));
            }
            else
            {
                TempData["error"] = (response?.Message ?? "") + " " + (response1?.Message ?? "");
            }
            AmenityVM amenityVM = new AmenityVM
            {
                VillaList = villas.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                Amenity = amenity
            };
            return View(amenityVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(AmenityVM amenityVM)
        {
            ResponseDTO? response = await _aminityService.UpdateAmenity(amenityVM.Amenity);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Amenity updated successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = response?.Message;
                string? userId = null;
                if (User.IsInRole(Constants.Role_Customer))
                {
                    userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                }
                ResponseDTO? response1 = await _villaService.GetAllVilla(userId);
                IEnumerable<Villa> listVilla = new List<Villa>();
                if (response1 != null && response1.IsSuccess)
                {
                    listVilla = JsonConvert.DeserializeObject<IEnumerable<Villa>>(Convert.ToString(response1.Result));
                }
                else
                {
                    TempData["error"] = response1?.Message;
                }
                amenityVM.VillaList = listVilla.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });
            }
            return View(amenityVM);
        }
        public async Task<IActionResult> Delete(int amenityId)
        {
            ResponseDTO? response = await _aminityService.GetAmenityById(amenityId);
            string? userId = null;
            if (User.IsInRole(Constants.Role_Customer))
            {
                userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            ResponseDTO? response1 = await _villaService.GetAllVilla(userId);
            Amenity amenity = null;
            List<Villa> villas = null;
            if (response != null && response.IsSuccess && response1 != null && response1.IsSuccess)
            {
                amenity = JsonConvert.DeserializeObject<Amenity>(Convert.ToString(response.Result));
                villas = JsonConvert.DeserializeObject<List<Villa>>(Convert.ToString(response1.Result));
            }
            else
            {
                TempData["error"] = (response?.Message ?? "") + " " + (response1?.Message ?? "");
            }
            AmenityVM amenityVM = new AmenityVM
            {
                VillaList = villas.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                Amenity = amenity
            };
            return View(amenityVM);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(AmenityVM amenity)
        {
            ResponseDTO? response = await _aminityService.DeleteAmenity(amenity.Amenity.Id);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Amenity updated successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(amenity);
        }
    }
}
