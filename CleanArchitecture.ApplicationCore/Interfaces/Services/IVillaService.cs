using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.ApplicationCore.Interfaces.Services
{
    public interface IVillaService
    {
        Task<ResponseDTO> GetAllVilla();
        Task<ResponseDTO> GetVilla(int villaId);
        Task<ResponseDTO> CreateVilla(Villa villa);
        Task<ResponseDTO> UpdateVilla(Villa villa);
        Task<ResponseDTO> DeleteVilla(int villaId);
        Task<ResponseDTO> GetAllDetailVilla();
       // Task<ResponseDTO> GetAvailableVillaByDate();
    }
}
