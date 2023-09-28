using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Entities;
using CleanArchitecture.ApplicationCore.Interfaces.Repositories;
using CleanArchitecture.ApplicationCore.Interfaces.Services;
using CleanArchitecture.ApplicationCore.Specifications;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.ApplicationCore.Services
{
    public class VillaNumberService : IVillaNumberService
    {
        private readonly IUnitOfWork _unitOfWork;
        private ResponseDTO _response;
        public VillaNumberService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _response = new ResponseDTO();
        }
        public async Task<ResponseDTO> CreateVillaNumber(VillaNumber villaNumber)
        {
            try
            {
                _response.Result = await _unitOfWork.villaNumberRepo.AddAsync(villaNumber);
            }catch(Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return _response;
        }

        public async Task<ResponseDTO> DeleteVillaNumber(int villaNumberId)
        {
            try
            {
                VillaNumber? deleteVillaNumber = await _unitOfWork.villaNumberRepo.GetByIdAsync(villaNumberId);
                 await _unitOfWork.villaNumberRepo.DeleteAsync(deleteVillaNumber);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return _response;
        }

        public async Task<ResponseDTO> GetAllVillaNumber(QueryParameter queryParameter)
        {
            try
            {
                var specification = new VillaNumberSpecification(queryParameter);                
                _response.Result = await _unitOfWork.villaNumberRepo.ListAsync(specification);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return _response;
        }

        public async Task<ResponseDTO> GetVillaNumber(int villaNumberId)
        {
            try
            {
                _response.Result = await _unitOfWork.villaNumberRepo.GetByIdAsync(villaNumberId);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return _response;
        }

        public async Task<ResponseDTO> UpdateVillaNumber(VillaNumber villaNumber)
        {
            try
            {
                await _unitOfWork.villaNumberRepo.UpdateAsync(villaNumber);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return _response;
        }
    }
}
