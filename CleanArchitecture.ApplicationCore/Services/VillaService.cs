using AutoMapper;
using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Entities;
using CleanArchitecture.ApplicationCore.Entities.DTOs;
using CleanArchitecture.ApplicationCore.Interfaces.Repositories;
using CleanArchitecture.ApplicationCore.Interfaces.Services;
using CleanArchitecture.ApplicationCore.Specifications;
using CleanArchitecture.ApplicationCore.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.ApplicationCore.Services
{
    public class VillaService : IVillaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private ResponseDTO _response;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IVillaNumberService _villaNumberService;
        public VillaService(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, IMapper mapper, IVillaNumberService villaNumberService)
        {
            _unitOfWork = unitOfWork;
            _response = new ResponseDTO();
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _villaNumberService = villaNumberService;
        }
        public async Task<ResponseDTO> CreateVilla(Villa villa)
        {
            try
            {
                if (villa.Image != null)
                {
                    string fileName = Guid.NewGuid() + Path.GetExtension(villa.Image.FileName);
                    string filePath = _webHostEnvironment.WebRootPath + @"/Images/" + fileName;
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await villa.Image.CopyToAsync(fileStream);
                    }
                    var urlFilePath = $"{_httpContextAccessor.HttpContext!.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/Images/{fileName}";
                    villa.ImageUrl = urlFilePath;
                    villa.ImageLocalPath = filePath;
                }
                else
                {
                    villa.ImageUrl = "https://placehold.co/600x400";
                    villa.ImageLocalPath = null;
                }
                _response.Result = await _unitOfWork.villaRepo.AddAsync(villa);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> DeleteVilla(int villaId)
        {
            try
            {
                Villa? deleteVila = await _unitOfWork.villaRepo.GetByIdAsync(villaId);
                if (deleteVila is null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Cannot find out the villa";
                }
                else
                {
                    if (!string.IsNullOrEmpty(deleteVila.ImageLocalPath))
                    {
                        FileInfo fileInfo = new FileInfo(deleteVila.ImageLocalPath);
                        if (fileInfo.Exists)
                        {
                            fileInfo.Delete();
                        }
                    }
                    await _unitOfWork.villaRepo.DeleteAsync(deleteVila);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> GetAllVilla(string? userId)
        {
            try
            {
                if (userId != null)
                {
                    var specification = new VillaSpecification(userId);
                    _response.Result = await _unitOfWork.villaRepo.ListAsync(specification);
                }
                else
                {
                    _response.Result = await _unitOfWork.villaRepo.ListAsync();
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> GetVilla(int villaId)
        {
            try
            {
                Villa? villa = await _unitOfWork.villaRepo.GetByIdAsync(villaId);
                VillaDTO villaDTO = _mapper.Map<VillaDTO>(villa);
                var specification = new AmenitySpecification(villaId);
                villaDTO.VillaAmenity = await _unitOfWork.amenityRepo.ListAsync(specification);
                _response.Result = villaDTO;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> UpdateVilla(Villa villa)
        {
            try
            {
                if (villa.Image is not null)
                {
                    if (!string.IsNullOrEmpty(villa.ImageLocalPath))
                    {
                        FileInfo fileInfo = new FileInfo(villa.ImageLocalPath);
                        if (fileInfo.Exists)
                        {
                            fileInfo.Delete();
                        }
                    }
                    string fileName = Guid.NewGuid() + Path.GetExtension(villa.Image.FileName);
                    string filePath = _webHostEnvironment.WebRootPath + @"/Images/" + fileName;
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await villa.Image.CopyToAsync(fileStream);
                    }
                    // var urlFilePath = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.PathBase}/Images/{fileName}";
                    var urlFilePath = $"{_httpContextAccessor.HttpContext!.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/Images/{fileName}";
                    villa.ImageUrl = urlFilePath;
                    villa.ImageLocalPath = filePath;
                }
                // _mapper.Map(productDTO, product);
                await _unitOfWork.villaRepo.UpdateAsync(villa);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> GetAllDetailVilla(int nights, long checkInDate)
        {
            try
            {
                DateTime dateTime = checkInDate.ToDateTime();

                DateOnly date = DateOnly.FromDateTime(dateTime);
                List<Villa> villas = await _unitOfWork.villaRepo.ListAsync();
                List<VillaDTO> villaDTO = _mapper.Map<List<VillaDTO>>(villas);
                foreach (var villa in villaDTO)
                {
                    var specification = new AmenitySpecification(villa.Id);
                    villa.VillaAmenity = await _unitOfWork.amenityRepo.ListAsync(specification);
                    var villaNumberList = await _unitOfWork.villaNumberRepo.ListAsync();
                    var bookingSpecification = new BookingFilterSpecification(PaymentStatus.StatusApproved, PaymentStatus.StatusCheckedIn);
                    var bookedVilla = await _unitOfWork.bookingRepo.ListAsync(bookingSpecification);
                    int roomAvailable = _villaNumberService.CountVillaRoomAvailable(villa.Id, villaNumberList, date, nights, bookedVilla);
                    villa.IsAvailable = roomAvailable > 0 ? true : false;
                }
                _response.Result = villaDTO;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> IsVillaAvailableByDate(int villaId, int nights, long checkInDate)
        {
            try
            {
                DateTime dateTime = checkInDate.ToDateTime();

                DateOnly date = DateOnly.FromDateTime(dateTime);
                var villaNumbersList = await _unitOfWork.villaNumberRepo.ListAsync();
                var bookingSpecification = new BookingFilterSpecification(PaymentStatus.StatusApproved, PaymentStatus.StatusCheckedIn);
                var bookedVillas = await _unitOfWork.bookingRepo.ListAsync(bookingSpecification);
                int roomAvailable = _villaNumberService.CountVillaRoomAvailable
                    (villaId, villaNumbersList, date, nights, bookedVillas);
                _response.IsSuccess = roomAvailable > 0 ? true : false;
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
