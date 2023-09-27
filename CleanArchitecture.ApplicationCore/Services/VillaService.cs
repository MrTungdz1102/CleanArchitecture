using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Entities;
using CleanArchitecture.ApplicationCore.Interfaces.Repositories;
using CleanArchitecture.ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.ApplicationCore.Services
{
    public class VillaService : IVillaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private ResponseDTO _response;
        public VillaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _response = new ResponseDTO();
        }
        public async Task<ResponseDTO> CreateVilla(Villa villa)
        {
            try
            {
                _response.Result = await _unitOfWork.villaRepo.AddAsync(villa);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> DeleteVilla(int villaId)
        {
            try
            {
                Villa? deleteVila = await _unitOfWork.villaRepo.GetByIdAsync(villaId);
                if (deleteVila is null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Cannot find out the villa";
                }
                else
                {
                    await _unitOfWork.villaRepo.DeleteAsync(deleteVila);
                }
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> GetAllVilla()
        {
            try
            {
                _response.Result = await _unitOfWork.villaRepo.ListAsync();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> GetVilla(int villaId)
        {
            try
            {
                _response.Result = await _unitOfWork.villaRepo.GetByIdAsync(villaId);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> UpdateVilla(Villa villa)
        {
            try
            {
                // _mapper.Map(productDTO, product);
                await _unitOfWork.villaRepo.UpdateAsync(villa);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
