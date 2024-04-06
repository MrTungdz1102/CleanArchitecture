using CleanArchitecture.WebUI.Models;
using CleanArchitecture.WebUI.Models.DTOs;
using CleanArchitecture.WebUI.Services.Interfaces;
using CleanArchitecture.WebUI.Utilities;

namespace CleanArchitecture.WebUI.Services.Implementations
{
	public class CityService : ICityService
	{
		private readonly IBaseService _baseService;

		public CityService(IBaseService baseService)
        {
			_baseService = baseService;
        }
        public async Task<ResponseDTO?> CreateCity(City city)
		{
			return await _baseService.SendAsync(new RequestDTO
			{
				Url = Constants.APIUrlBase + "/api/CityAPI/CreateCity",
				Data = city,
				ApiType = Constants.ApiType.POST
			});
		}

		public async Task<ResponseDTO?> DeleteCity(int cityId)
		{
			return await _baseService.SendAsync(new RequestDTO
			{
				Url = Constants.APIUrlBase + "/api/CityAPI/DeleteCity/" + cityId,
				ApiType = Constants.ApiType.DELETE
			});
		}

		public async Task<ResponseDTO?> GetAllCity()
		{
			return await _baseService.SendAsync(new RequestDTO
			{
				Url = Constants.APIUrlBase + "/api/CityAPI/GetAllCity",
				ApiType = Constants.ApiType.GET
			});
		}

		public async Task<ResponseDTO?> GetCity(int cityId)
		{
			return await _baseService.SendAsync(new RequestDTO
			{
				Url = Constants.APIUrlBase + "/api/CityAPI/GetCity/" + cityId,
				ApiType = Constants.ApiType.GET
			});
		}

		public async Task<ResponseDTO?> UpdateCity(City city)
		{
			return await _baseService.SendAsync(new RequestDTO
			{
				Url = Constants.APIUrlBase + "/api/CityAPI/UpdateCity",
				Data = city,
				ApiType = Constants.ApiType.PUT
			});
		}
	}
}
