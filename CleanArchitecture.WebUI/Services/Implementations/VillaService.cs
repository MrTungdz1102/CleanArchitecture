using CleanArchitecture.WebUI.Models;
using CleanArchitecture.WebUI.Models.DTOs;
using CleanArchitecture.WebUI.Services.Interfaces;
using CleanArchitecture.WebUI.Utilities;

namespace CleanArchitecture.WebUI.Services.Implementations
{
    public class VillaService : IVillaService
    {
        private readonly IBaseService _baseService;
        public VillaService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDTO?> CreateVilla(Villa villa)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                Url = Constants.APIUrlBase + "/api/VillaAPI/CreateVilla",
                Data = villa,
                ApiType = Constants.ApiType.POST,
                ContentType = Constants.ContentType.MultipartFormData
            });
        }

        public async Task<ResponseDTO?> DeleteVilla(int id)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                Url = Constants.APIUrlBase + "/api/VillaAPI/DeleteVilla/" + id,
                ApiType = Constants.ApiType.DELETE
            });
        }

        public async Task<ResponseDTO?> GetAllDetailVilla()
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                Url = Constants.APIUrlBase + "/api/VillaAPI/GetAllDetailVilla",
                ApiType = Constants.ApiType.GET
            });
        }

        public async Task<ResponseDTO?> GetAllVilla()
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                Url = Constants.APIUrlBase + "/api/VillaAPI/GetAllVilla",
                ApiType = Constants.ApiType.GET
            });
        }

        public async Task<ResponseDTO?> GetVillaById(int id)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                Url = Constants.APIUrlBase + "/api/VillaAPI/GetVilla/" + id,
                ApiType = Constants.ApiType.GET
            });
        }

        public async Task<ResponseDTO?> UpdateVilla(Villa villa)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                Url = Constants.APIUrlBase + "/api/VillaAPI/UpdateVilla",
                ApiType = Constants.ApiType.PUT,
                Data = villa,
                ContentType = Constants.ContentType.MultipartFormData
            });
        }
    }
}
