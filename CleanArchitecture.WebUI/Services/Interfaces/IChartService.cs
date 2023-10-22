using CleanArchitecture.WebUI.Models.DTOs;

namespace CleanArchitecture.WebUI.Services.Interfaces
{
    public interface IChartService
    {
        Task<ResponseDTO?> GetBookingPieChart();
        Task<ResponseDTO?> GetMemberAndBookingLineChart();
        Task<ResponseDTO?> GetRegisterUserChart();
        Task<ResponseDTO?> GetRevenueChart();
        Task<ResponseDTO?> GetTotalBookingRadialChart();
    }
}
