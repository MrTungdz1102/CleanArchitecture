using CleanArchitecture.WebUI.Models;
using CleanArchitecture.WebUI.Models.DTOs;
using CleanArchitecture.WebUI.Services.Interfaces;
using CleanArchitecture.WebUI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CleanArchitecture.WebUI.Controllers
{
    [Authorize(Roles = Constants.Role_Admin + "," + Constants.Role_Manager)]
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        public async Task<IActionResult> Index()
        {
            List<Coupon>? coupons = new List<Coupon>();
            ResponseDTO? response = await _couponService.GetAllCoupon();
            if (response is not null && response.IsSuccess)
            {
                coupons = JsonConvert.DeserializeObject<List<Coupon>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(coupons);
        }

        public IActionResult CreateCoupon()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCoupon(Coupon coupon)
        {
            ResponseDTO? response = await _couponService.CreateCoupon(coupon);
            if (response is not null && response.IsSuccess)
            {
                TempData["success"] = "Coupon created successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(coupon);
        }

        public async Task<IActionResult> UpdateCoupon(int id)
        {
            Coupon? coupon = null;
            ResponseDTO? response = await _couponService.GetCouponById(id);
            if (response is not null && response.IsSuccess)
            {
                coupon = JsonConvert.DeserializeObject<Coupon>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(coupon);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCoupon(Coupon coupon)
        {
            ResponseDTO? response = await _couponService.UpdateCoupon(coupon);
            if (response is not null && response.IsSuccess)
            {
                TempData["success"] = "Coupon updated successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(coupon);
        }

        public async Task<IActionResult> DeleteCoupon(int id)
        {
            Coupon? coupon = null;
            ResponseDTO? response = await _couponService.GetCouponById(id);
            if (response != null && response.IsSuccess)
            {
                coupon = JsonConvert.DeserializeObject<Coupon>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(coupon);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCoupon(Coupon coupon)
        {
            ResponseDTO? response = await _couponService.DeleteCoupon(coupon.CouponId);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Coupon deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(coupon);
        }

    }
}
