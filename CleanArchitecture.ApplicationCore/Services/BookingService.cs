using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Entities;
using CleanArchitecture.ApplicationCore.Interfaces.Repositories;
using CleanArchitecture.ApplicationCore.Interfaces.Services;
using CleanArchitecture.ApplicationCore.Specifications;

namespace CleanArchitecture.ApplicationCore.Services
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private ResponseDTO _response;
        private readonly IVillaNumberService _villaNumberService;
        public BookingService(IUnitOfWork unitOfWork, IVillaNumberService villaNumberService)
        {
            _unitOfWork = unitOfWork;
            _response = new ResponseDTO();
            _villaNumberService = villaNumberService;
        }
        public async Task<ResponseDTO> CreateBooking(Booking booking)
        {
            try
            {
                _response.Result = await _unitOfWork.bookingRepo.AddAsync(booking);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> GetAllBooking(string? statusFilter)
        {
            try
            {
                var specification = new BookingSpecification(statusFilter);
                _response.Result = await _unitOfWork.bookingRepo.ListAsync(specification);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> GetAllBookingUser(string userId, string? statusFilter, bool isCustomer = false)
        {
            try
            {
                var specification = new BookingSpecification(userId, statusFilter, isCustomer);
                _response.Result = await _unitOfWork.bookingRepo.ListAsync(specification);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> GetBooking(int bookingId)
        {
            try
            {
                var specification = new BookingSpecification(bookingId);
                Booking? booking = await _unitOfWork.bookingRepo.FirstOrDefaultAsync(specification);
                if (booking.VillaNumber == 0 && booking.Status == PaymentStatus.StatusApproved)
                {
                    var availableVillaNumber = await _villaNumberService.AssignAvailableVillaNumberByVilla(booking.VillaId);
                    var specificationVillaNumber = new VillaNumberSpecification(booking.VillaId, availableVillaNumber);
                    booking.VillaNumbers = await _unitOfWork.villaNumberRepo.ListAsync(specificationVillaNumber);
                }
                _response.Result = booking;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> UpdatePayment(int bookingId, string sessionId, string paymentIntentId)
        {
            try
            {
                Booking? booking = await _unitOfWork.bookingRepo.GetByIdAsync(bookingId);
                if (booking is not null)
                {
                    if (!string.IsNullOrEmpty(sessionId))
                    {
                        booking.StripeSessionId = sessionId;
                    }
                    if (!string.IsNullOrEmpty(paymentIntentId))
                    {
                        booking.StripePaymentIntentId = paymentIntentId;
                        booking.PaymentDate = DateTime.Now;
                        booking.IsPaymentSuccessful = true;
                    }
                    await _unitOfWork.bookingRepo.UpdateAsync(booking);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.Message = "Not Found Booking!";
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> UpdateStatus(int bookingId, string status, int villaNumber = 0)
        {
            try
            {
                Booking? booking = await _unitOfWork.bookingRepo.GetByIdAsync(bookingId);
                if (booking is not null)
                {
                    booking.Status = status;
                    if (status == PaymentStatus.StatusCheckedIn)
                    {
                        booking.VillaNumber = villaNumber;
                        booking.ActualCheckInDate = DateTime.Now;
                    }
                    if (status == PaymentStatus.StatusCompleted)
                    {
                        booking.ActualCheckOutDate = DateTime.Now;
                    }
                    await _unitOfWork.bookingRepo.UpdateAsync(booking);
                }
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
