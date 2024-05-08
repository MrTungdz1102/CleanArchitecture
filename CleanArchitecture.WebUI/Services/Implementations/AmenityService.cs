using CleanArchitecture.WebUI.Models;
using CleanArchitecture.WebUI.Models.DTOs;
using CleanArchitecture.WebUI.Models.ViewModel;
using CleanArchitecture.WebUI.Services.Interfaces;
using CleanArchitecture.WebUI.Utilities;

namespace CleanArchitecture.WebUI.Services.Implementations
{
    public class AmenityService : IAmenityService
    {
        private readonly IBaseService _baseService;

        public AmenityService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDTO?> CreateAmenity(Amenity amenity)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                Url = Constants.APIUrlBase + "/api/AmenityAPI/CreateAmenity",
                Data = amenity,
                ApiType = Constants.ApiType.POST
            });
        }

        public async Task<ResponseDTO?> DeleteAmenity(int id)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                Url = Constants.APIUrlBase + "/api/AmenityAPI/DeleteAmenity/" + id,
                ApiType = Constants.ApiType.DELETE
            });
        }

        public async Task<ResponseDTO?> GetAllAmenity(QueryParameter queryParameter, string? userId)
        {
            string apiUrl = Constants.APIUrlBase + "/api/AmenityAPI/GetAllAmenity?PageNumber=" + queryParameter.PageNumber + "&PageSize=" + queryParameter.PageSize;
            if (!string.IsNullOrEmpty(userId))
            {
                apiUrl += $"&userId={userId}";
            }
            return await _baseService.SendAsync(new RequestDTO
            {
                Url = apiUrl,
                ApiType = Constants.ApiType.GET
            });
        }

        public async Task<ResponseDTO?> GetAmenityById(int id)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                Url = Constants.APIUrlBase + "/api/AmenityAPI/GetAmenity/" + id,
                ApiType = Constants.ApiType.GET
            });
        }

        public async Task<ResponseDTO?> UpdateAmenity(Amenity amenity)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                Url = Constants.APIUrlBase + "/api/AmenityAPI/UpdateAmenity",
                ApiType = Constants.ApiType.PUT,
                Data = amenity
            });
        }
    }
}
