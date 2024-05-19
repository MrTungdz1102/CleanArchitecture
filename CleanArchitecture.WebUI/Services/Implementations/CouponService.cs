using CleanArchitecture.WebUI.Models;
using CleanArchitecture.WebUI.Models.DTOs;
using CleanArchitecture.WebUI.Services.Interfaces;
using CleanArchitecture.WebUI.Utilities;

namespace CleanArchitecture.WebUI.Services.Implementations
{
    public class CouponService : ICouponService
    {
        private readonly IBaseService _baseService;
        public CouponService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDTO?> CreateCoupon(Coupon coupon)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                Url = Constants.APIUrlBase + "/api/CouponAPI/CreateCoupon",
                Data = coupon,
                ApiType = Constants.ApiType.POST
            });
        }

        public async Task<ResponseDTO?> DeleteCoupon(int couponId)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                Url = Constants.APIUrlBase + "/api/CouponAPI/DeleteCoupon/" + couponId,
                ApiType = Constants.ApiType.DELETE
            });
        }

        public async Task<ResponseDTO?> GetAllCoupon()
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                Url = Constants.APIUrlBase + "/api/CouponAPI/GetAllCoupon",
                ApiType = Constants.ApiType.GET
            });
        }

        public async Task<ResponseDTO?> GetCouponByCode(string couponCode)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                Url = Constants.APIUrlBase + "/api/CouponAPI/GetCouponByCode/" + couponCode,
                ApiType = Constants.ApiType.GET
            });
        }

        public async Task<ResponseDTO?> GetCouponById(int couponId)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                Url = Constants.APIUrlBase + "/api/CouponAPI/GetCouponById/" + couponId,
                ApiType = Constants.ApiType.GET
            });
        }

        public async Task<ResponseDTO?> UpdateCoupon(Coupon coupon)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                Url = Constants.APIUrlBase + "/api/CouponAPI/UpdateCoupon",
                Data = coupon,
                ApiType = Constants.ApiType.PUT
            });
        }
    }
}
