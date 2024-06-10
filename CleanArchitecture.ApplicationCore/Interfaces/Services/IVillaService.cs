using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Entities;

namespace CleanArchitecture.ApplicationCore.Interfaces.Services
{
    public interface IVillaService
    {
        Task<ResponseDTO> GetAllVilla(string? userId);
        Task<ResponseDTO> GetVilla(int villaId);
        Task<ResponseDTO> CreateVilla(Villa villa);
        Task<ResponseDTO> UpdateVilla(Villa villa);
        Task<ResponseDTO> DeleteVilla(int villaId);
        Task<ResponseDTO> GetAllDetailVilla(int nights, long checkInDate, string? keyword, int? cityId, double? priceFrom, double? priceTo);
        Task<ResponseDTO> IsVillaAvailableByDate(int villaId, int nights, long checkInDate);
    }
}
