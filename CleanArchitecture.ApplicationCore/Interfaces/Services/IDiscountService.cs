using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.ApplicationCore.Interfaces.Services
{
    public interface IDiscountService
    {
        Task<ResponseDTO> GetAllCoupon();
        Task<ResponseDTO> GetCouponById(int couponId);
        Task<ResponseDTO> GetCouponByCode(string couponCode);
        Task<ResponseDTO> CreateCoupon(Coupon coupon);
        Task<ResponseDTO> UpdateCoupon(Coupon coupon);
        Task<ResponseDTO> DeleteCoupon(int couponId);
    }
}
