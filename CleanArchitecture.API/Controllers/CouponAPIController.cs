using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Entities;
using CleanArchitecture.ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace CleanArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        private readonly IDiscountService _discountService;

        public CouponAPIController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        [HttpGet("GetAllCoupon")]
        public async Task<ActionResult<ResponseDTO>> GetAllCoupon()
        {
            return Ok(await _discountService.GetAllCoupon());
        }

        [HttpGet("GetCouponById/{id:int}")]
        public async Task<ActionResult<ResponseDTO>> GetCouponById(int couponId)
        {
            return Ok(await _discountService.GetCouponById(couponId));
        }

        [HttpGet("GetByCode/{code}")]
        public async Task<ActionResult<ResponseDTO>> GetByCode(string couponCode)
        {
            return Ok(await _discountService.GetCouponByCode(couponCode));
        }

        [HttpPost("CreateCoupon")]
        // [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<ResponseDTO>> CreateCoupon([FromBody] ApplicationCore.Entities.Coupon coupon)
        {
            return Ok(await _discountService.CreateCoupon(coupon));
        }

        [HttpPut("UpdateCoupon/{id:int}")]
        public async Task<ActionResult<ResponseDTO>> UpdateCoupon([FromBody] ApplicationCore.Entities.Coupon coupon)
        {
            return Ok(await _discountService.UpdateCoupon(coupon));
        }

        [HttpDelete("DeleteCoupon/{id:int}")]
        public async Task<ActionResult<ResponseDTO>> DeleteCoupon(int couponId)
        {
            return Ok(await _discountService.DeleteCoupon(couponId));
        }
    }
}
