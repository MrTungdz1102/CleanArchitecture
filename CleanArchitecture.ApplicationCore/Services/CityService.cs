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
    public class CityService : ICityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private ResponseDTO _response;
        public CityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _response = new ResponseDTO();
        }
        public async Task<ResponseDTO> CreateCity(City city)
        {
            try
            {
                _response.Result = await _unitOfWork.cityRepo.AddAsync(city);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> DeleteCity(int cityId)
        {
            try
            {
                City? deleteCity = await _unitOfWork.cityRepo.GetByIdAsync(cityId);
                if (deleteCity is null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Cannot find out the city";
                }
                else
                {
                    await _unitOfWork.cityRepo.DeleteAsync(deleteCity);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> GetAllCity()
        {
            try
            {
                _response.Result = await _unitOfWork.cityRepo.ListAsync();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> GetCity(int cityId)
        {
            try
            {
                _response.Result = await _unitOfWork.cityRepo.GetByIdAsync(cityId);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> UpdateCity(City city)
        {
            try
            {
                await _unitOfWork.cityRepo.UpdateAsync(city);
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
