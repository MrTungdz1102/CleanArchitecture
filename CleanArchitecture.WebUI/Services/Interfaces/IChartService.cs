using CleanArchitecture.WebUI.Models.DTOs;

namespace CleanArchitecture.WebUI.Services.Interfaces
{
    public interface IChartService
    {
        Task<ResponseDTO?> GetBookingPieChart(string? ownerId);
        Task<ResponseDTO?> GetMemberAndBookingLineChart(string? ownerId);
        Task<ResponseDTO?> GetRegisterUserChart();
        Task<ResponseDTO?> GetRevenueChart(string? ownerId);
        Task<ResponseDTO?> GetTotalBookingRadialChart(string? ownerId);
    }
}
