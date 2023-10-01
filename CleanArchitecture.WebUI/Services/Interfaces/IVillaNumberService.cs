using CleanArchitecture.WebUI.Models;
using CleanArchitecture.WebUI.Models.DTOs;
using CleanArchitecture.WebUI.Utilities;

namespace CleanArchitecture.WebUI.Services.Interfaces
{
    public interface IVillaNumberService
    {
        Task<ResponseDTO?> GetAllVillaNumber(QueryParameter queryParameter);
        Task<ResponseDTO?> GetVillaNumberById(int id);
        Task<ResponseDTO?> CreateVillaNumber(VillaNumber villaNumber);
        Task<ResponseDTO?> UpdateVillaNumber(VillaNumber villaNumber);
        Task<ResponseDTO?> DeleteVillaNumber(int id);
    }
}
