using CleanArchitecture.ApplicationCore.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.ApplicationCore.Interfaces.Services
{
    public interface IChartService
    {
        Task<ResponseDTO> GetTotalBookingRadialChartData(string? ownerId = null);
        Task<ResponseDTO> GetRegisteredUserChartData();
        Task<ResponseDTO> GetRevenueChartData(string? ownerId = null);
        Task<ResponseDTO> GetBookingPieChartData(string? ownerId = null);
        Task<ResponseDTO> GetMemberAndBookingLineChartData(string? ownerId = null);
        RadialBarChart GetRadialCartDataModel(int totalCount, double currentMonthCount, double prevMonthCount);
    }
}
