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
        Task<ResponseDTO> GetTotalBookingRadialChartData();
        Task<ResponseDTO> GetRegisteredUserChartData();
        Task<ResponseDTO> GetRevenueChartData();
        Task<ResponseDTO> GetBookingPieChartData();
        Task<ResponseDTO> GetMemberAndBookingLineChartData();
        RadialBarChart GetRadialCartDataModel(int totalCount, double currentMonthCount, double prevMonthCount);
    }
}
