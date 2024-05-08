using CleanArchitecture.WebUI.Models.DTOs;
using CleanArchitecture.WebUI.Services.Interfaces;
using CleanArchitecture.WebUI.Utilities;

namespace CleanArchitecture.WebUI.Services.Implementations
{
    public class ChartService : IChartService
    {
        private readonly IBaseService _baseSerivce;

        public ChartService(IBaseService baseService)
        {
            _baseSerivce = baseService;
        }
        public async Task<ResponseDTO?> GetBookingPieChart(string? ownerId)
        {
            string apiUrl = Constants.APIUrlBase + "/api/chartAPI/GetBookingPieChart";
            if (!string.IsNullOrEmpty(ownerId))
            {
                apiUrl += $"?ownerId={ownerId}";
            }
            return await _baseSerivce.SendAsync(new RequestDTO
            {
                Url = apiUrl,
                ApiType = Constants.ApiType.GET
            });
        }

        public async Task<ResponseDTO?> GetMemberAndBookingLineChart(string? ownerId)
        {
            string apiUrl = Constants.APIUrlBase + "/api/chartAPI/GetMemberAndBookingLineChart";
            if (!string.IsNullOrEmpty(ownerId))
            {
                apiUrl += $"?ownerId={ownerId}";
            }
            return await _baseSerivce.SendAsync(new RequestDTO
            {
                Url = apiUrl,
                ApiType = Constants.ApiType.GET
            });
        }

        public async Task<ResponseDTO?> GetRegisterUserChart()
        {
            return await _baseSerivce.SendAsync(new RequestDTO
            {
                Url = Constants.APIUrlBase + "/api/chartAPI/GetRegisterUserChart",
                ApiType = Constants.ApiType.GET
            });
        }

        public async Task<ResponseDTO?> GetRevenueChart(string? ownerId)
        {
            string apiUrl = Constants.APIUrlBase + "/api/chartAPI/GetRevenueChart";
            if (!string.IsNullOrEmpty(ownerId))
            {
                apiUrl += $"?ownerId={ownerId}";
            }
            return await _baseSerivce.SendAsync(new RequestDTO
            {
                Url = apiUrl,
                ApiType = Constants.ApiType.GET
            });
        }

        public async Task<ResponseDTO?> GetTotalBookingRadialChart(string? ownerId)
        {
            string apiUrl = Constants.APIUrlBase + "/api/chartAPI/GetTotalBookingRadialChart";
            if (!string.IsNullOrEmpty(ownerId))
            {
                apiUrl += $"?ownerId={ownerId}";
            }
            return await _baseSerivce.SendAsync(new RequestDTO
            {
                Url = apiUrl,
                ApiType = Constants.ApiType.GET
            });
        }
    }
}
