using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Entities;
using CleanArchitecture.ApplicationCore.Interfaces.Repositories;
using CleanArchitecture.ApplicationCore.Interfaces.Services;
using CleanArchitecture.ApplicationCore.Specifications;

namespace CleanArchitecture.ApplicationCore.Services
{
    public class AmenityService : IAmenityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private ResponseDTO _response;
        public AmenityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _response = new ResponseDTO();
        }
        public async Task<ResponseDTO> CreateAmenity(Amenity amenity)
        {
            try
            {
                _response.Result = await _unitOfWork.amenityRepo.AddAsync(amenity);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> DeleteAmenity(int amenityId)
        {
            try
            {
                Amenity? deleteAmenity = await _unitOfWork.amenityRepo.GetByIdAsync(amenityId);
                if (deleteAmenity is null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Cannot find out the villa";
                }
                else
                {
                    await _unitOfWork.amenityRepo.DeleteAsync(deleteAmenity);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> GetAllAmenity(QueryParameter query, string? userId)
        {
            try
            {
                var totalSize = await _unitOfWork.amenityRepo.CountAsync();
                var specification = new AmenitySpecification(query, userId);
                List<Amenity> amenities = await _unitOfWork.amenityRepo.ListAsync(specification);
                _response.Result = new PageResult<Amenity>
                {
                    Items = amenities,
                    TotalCount = totalSize,
                    PageNumber = query.PageNumber,
                    RecordNumber = query.PageSize
                };
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> GetAmenity(int amenityId)
        {
            try
            {
                _response.Result = await _unitOfWork.amenityRepo.GetByIdAsync(amenityId);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> UpdateAmenity(Amenity amenity)
        {
            try
            {
                await _unitOfWork.amenityRepo.UpdateAsync(amenity);
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
