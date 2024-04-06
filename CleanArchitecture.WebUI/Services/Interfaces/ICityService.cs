using CleanArchitecture.WebUI.Models;
using CleanArchitecture.WebUI.Models.DTOs;

namespace CleanArchitecture.WebUI.Services.Interfaces
{
	public interface ICityService
	{
		Task<ResponseDTO?> GetAllCity();
		Task<ResponseDTO?> GetCity(int cityId);
		Task<ResponseDTO?> CreateCity(City city);
		Task<ResponseDTO?> UpdateCity(City city);
		Task<ResponseDTO?> DeleteCity(int cityId);
	}
}
