using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.ApplicationCore.Interfaces.Services
{
    public interface IAmenityService
    {
        Task<ResponseDTO> GetAllAmenity(QueryParameter query);
        Task<ResponseDTO> GetAmenity(int amenityId);
        Task<ResponseDTO> CreateAmenity(Amenity amenity);
        Task<ResponseDTO> UpdateAmenity(Amenity amenity);
        Task<ResponseDTO> DeleteAmenity(int amenityId);
    }
}
