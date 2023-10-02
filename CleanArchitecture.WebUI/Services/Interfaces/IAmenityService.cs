using CleanArchitecture.WebUI.Models;
using CleanArchitecture.WebUI.Models.DTOs;
using CleanArchitecture.WebUI.Utilities;

namespace CleanArchitecture.WebUI.Services.Interfaces
{
    public interface IAmenityService
    {
        Task<ResponseDTO?> GetAllAmenity(QueryParameter queryParameter);
        Task<ResponseDTO?> GetAmenityById(int id);
        Task<ResponseDTO?> CreateAmenity(Amenity amenity);
        Task<ResponseDTO?> UpdateAmenity(Amenity amenity);
        Task<ResponseDTO?> DeleteAmenity(int id);
    }
}
