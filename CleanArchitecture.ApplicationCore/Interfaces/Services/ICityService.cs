using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.ApplicationCore.Interfaces.Services
{
    public interface ICityService
    {
        Task<ResponseDTO> GetAllCity();
        Task<ResponseDTO> GetCity(int cityId);
        Task<ResponseDTO> CreateCity(City city);
        Task<ResponseDTO> UpdateCity(City city);
        Task<ResponseDTO> DeleteCity(int cityId);
    }
}
