using CleanArchitecture.WebUI.Models;
using CleanArchitecture.WebUI.Models.DTOs;

namespace CleanArchitecture.WebUI.Services.Interfaces
{
    public interface ICouponService
    {
        Task<ResponseDTO?> GetAllCoupon();
        Task<ResponseDTO?> GetCouponById(int couponId);
        Task<ResponseDTO?> GetCouponByCode(string couponCode);
        Task<ResponseDTO?> CreateCoupon(Coupon coupon);
        Task<ResponseDTO?> UpdateCoupon(Coupon coupon);
        Task<ResponseDTO?> DeleteCoupon(int couponId);
    }
}
