using CleanArchitecture.WebUI.Models;
using CleanArchitecture.WebUI.Models.DTOs;

namespace CleanArchitecture.WebUI.Services.Interfaces
{
    public interface IVillaService
    {
        Task<ResponseDTO?> GetAllVilla(); 
        Task<ResponseDTO?> GetVillaById(int id);
        Task<ResponseDTO?> CreateVilla(Villa villa);
        Task<ResponseDTO?> UpdateVilla(Villa villa);
        Task<ResponseDTO?> DeleteVilla(int id);
    }
}
