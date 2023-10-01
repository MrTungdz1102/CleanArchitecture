using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.ApplicationCore.Interfaces.Services
{
    public interface IVillaNumberService
    {
        Task<ResponseDTO> GetAllVillaNumber(QueryParameter queryParameter);
        Task<ResponseDTO> GetVillaNumber(int villaNumberId);
        Task<ResponseDTO> CreateVillaNumber(VillaNumber villaNumber);
        Task<ResponseDTO> UpdateVillaNumber(VillaNumber villaNumber);
        Task<ResponseDTO> DeleteVillaNumber(int villaNumberId);
        Task<bool> CheckVillaNumberExits(int villaNumberId);
    }
}
