using CleanArchitecture.WebUI.Models;
using CleanArchitecture.WebUI.Models.DTOs;

namespace CleanArchitecture.WebUI.Services.Interfaces
{
    public interface IVillaService
    {
        Task<ResponseDTO?> GetAllVilla(string? userId);
        Task<ResponseDTO?> GetVillaById(int id);
        Task<ResponseDTO?> CreateVilla(Villa villa);
        Task<ResponseDTO?> UpdateVilla(Villa villa);
        Task<ResponseDTO?> DeleteVilla(int id);
        Task<ResponseDTO?> GetAllDetailVilla(int nights, long checkInDate);
        Task<ResponseDTO?> IsVillaAvailableByDate(int villaId, int nights, long checkInDate);
    }
}
