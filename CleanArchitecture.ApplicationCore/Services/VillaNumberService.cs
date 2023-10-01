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

        public async Task<bool> CheckVillaNumberExits(int villaNumberId)
        {
            var specification = new VillaNumberSpecification(villaNumberId);
            return await _unitOfWork.villaNumberRepo.AnyAsync(specification);
        }

        public async Task<ResponseDTO> CreateVillaNumber(VillaNumber villaNumber)
        {
            try
            {
                bool roomNumberExists  = await CheckVillaNumberExits(villaNumber.Villa_Number);
                if (!roomNumberExists)
                {
                    _response.Result = await _unitOfWork.villaNumberRepo.AddAsync(villaNumber);
                }
                else
                {
                    _response.Message = "Room was exist";
                    _response.IsSuccess = false;
                }
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
                var totalSize = await _unitOfWork.villaNumberRepo.CountAsync();
                var specification = new VillaNumberSpecification(queryParameter);                
                List<VillaNumber> villas = await _unitOfWork.villaNumberRepo.ListAsync(specification);
                _response.Result = new PageResult<VillaNumber>
                {
                    Items = villas,
                    TotalCount = totalSize,
                    PageNumber = queryParameter.PageNumber,
                    RecordNumber = queryParameter.PageSize
                };
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
