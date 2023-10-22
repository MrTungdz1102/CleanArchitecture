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
        public async Task<ResponseDTO?> GetBookingPieChart()
        {
            return await _baseSerivce.SendAsync(new RequestDTO
            {
                Url = Constants.APIUrlBase + "/api/chartAPI/GetBookingPieChart",
                ApiType = Constants.ApiType.GET
            });
        }

        public async Task<ResponseDTO?> GetMemberAndBookingLineChart()
        {
            return await _baseSerivce.SendAsync(new RequestDTO
            {
                Url = Constants.APIUrlBase + "/api/chartAPI/GetMemberAndBookingLineChart",
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

        public async Task<ResponseDTO?> GetRevenueChart()
        {
            return await _baseSerivce.SendAsync(new RequestDTO
            {
                Url = Constants.APIUrlBase + "/api/chartAPI/GetRevenueChart",
                ApiType = Constants.ApiType.GET
            });
        }

        public async Task<ResponseDTO?> GetTotalBookingRadialChart()
        {
            return await _baseSerivce.SendAsync(new RequestDTO
            {
                Url = Constants.APIUrlBase + "/api/chartAPI/GetTotalBookingRadialChart",
                ApiType = Constants.ApiType.GET
            });
        }
    }
}
