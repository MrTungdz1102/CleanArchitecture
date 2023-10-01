using CleanArchitecture.WebUI.Models;
using CleanArchitecture.WebUI.Models.DTOs;

namespace CleanArchitecture.WebUI.Services.Interfaces
{
    public interface IVillaNumberService
    {
        Task<ResponseDTO?> GetAllVillaNumber();
        Task<ResponseDTO?> GetVillaNumberById(int id);
        Task<ResponseDTO?> CreateVillaNumber(VillaNumber villaNumber);
        Task<ResponseDTO?> UpdateVillaNumber(VillaNumber villaNumber);
        Task<ResponseDTO?> DeleteVillaNumber(int id);
    }
}
