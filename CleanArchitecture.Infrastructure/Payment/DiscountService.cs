using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Entities;
using CleanArchitecture.ApplicationCore.Interfaces.Repositories;
using CleanArchitecture.ApplicationCore.Interfaces.Services;
using CleanArchitecture.ApplicationCore.Specifications;

namespace CleanArchitecture.Infrastructure.Payment
{
    public class DiscountService : IDiscountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private ResponseDTO _response;
        public DiscountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _response = new ResponseDTO();
        }
        public async Task<ResponseDTO> CreateCoupon(ApplicationCore.Entities.Coupon coupon)
        {
            try
            {
                //TimeSpan timeSpan = coupon.EndingDate - coupon.StartingDate;
                //int totalDays = (int)timeSpan.TotalDays;
                //long DurationInMonths = (long)(totalDays + 30) / 30;

                var options = new Stripe.CouponCreateOptions
                {
                    AmountOff = (long)coupon.DiscountAmount * 100,
                    Name = coupon.CouponCode,
                    Currency = "USD",
                    Id = coupon.CouponCode,
                   // DurationInMonths = DurationInMonths
                };
                var service = new Stripe.CouponService();
                await service.CreateAsync(options);
                _response.Result = await _unitOfWork.couponRepo.AddAsync(coupon);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> DeleteCoupon(int couponId)
        {
            try
            {
                ApplicationCore.Entities.Coupon? deleteCoupon = await _unitOfWork.couponRepo.GetByIdAsync(couponId);
                if(deleteCoupon is null) {
                    _response.IsSuccess = false;
                    _response.Message = "Cannot find out the coupon";
                }
                else
                {
                    await _unitOfWork.couponRepo.DeleteAsync(deleteCoupon);
                    var service = new Stripe.CouponService();
                    await service.DeleteAsync(deleteCoupon.CouponCode);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> GetAllCoupon()
        {
            try
            {
                _response.Result = await _unitOfWork.couponRepo.ListAsync();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> GetCouponByCode(string couponCode)
        {
            try
            {
                var specification = new CouponSpecification(couponCode);
                _response.Result = await _unitOfWork.couponRepo.FirstOrDefaultAsync(specification);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> GetCouponById(int couponId)
        {
            try
            {
                _response.Result = await _unitOfWork.couponRepo.GetByIdAsync(couponId);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> UpdateCoupon(ApplicationCore.Entities.Coupon coupon)
        {
            try
            {
                var options = new Stripe.CouponUpdateOptions
                {
                    Name = coupon.CouponCode
                };
                var service = new Stripe.CouponService();
                await service.UpdateAsync(coupon.CouponCode, options);
                await _unitOfWork.couponRepo.UpdateAsync(coupon);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
