using CleanArchitecture.WebUI.Models;
using CleanArchitecture.WebUI.Models.DTOs;
using CleanArchitecture.WebUI.Services.Interfaces;
using CleanArchitecture.WebUI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace CleanArchitecture.WebUI.Controllers
{
    [Authorize]
    public class VillaController : Controller
    {
        private readonly IVillaService _villaService;
        public VillaController(IVillaService villaService)
        {
            _villaService = villaService;
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
            if (response != null && response.IsSuccess) {
                villaList = JsonConvert.DeserializeObject<List<Villa>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
                return RedirectToAction("Index", "Home");
            }
            return View(villaList);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Villa villa)
        {
            ResponseDTO? response = await _villaService.CreateVilla(villa);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Villa created successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(villa);
        }

        public async Task<IActionResult> Update(int villaId)
        {
            Villa? villa = null;
            ResponseDTO? response = await _villaService.GetVillaById(villaId);
            if (response != null && response.IsSuccess)
            {
                villa = JsonConvert.DeserializeObject<Villa>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(villa);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Villa villa)
        {
            ResponseDTO? response = await _villaService.UpdateVilla(villa);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Villa updated successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(villa);
        }
        public async Task<IActionResult> Delete(int villaId)
        {
            Villa? villa = null;
            ResponseDTO? response = await _villaService.GetVillaById(villaId);
            if (response != null && response.IsSuccess)
            {
                villa = JsonConvert.DeserializeObject<Villa>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(villa);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Villa villa)
        {
            ResponseDTO? response = await _villaService.DeleteVilla(villa.Id);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Villa deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(villa);
        }
    }
}
