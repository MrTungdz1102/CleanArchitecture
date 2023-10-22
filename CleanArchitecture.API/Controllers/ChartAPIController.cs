using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartAPIController : ControllerBase
    {
        private readonly IChartService _chartSerice;

        public ChartAPIController(IChartService chartService)
        {
            _chartSerice = chartService;
        }

        [HttpGet("GetBookingPieChart")]
        public async Task<ActionResult<ResponseDTO>> GetBookingPieChart()
        {
            return Ok(await _chartSerice.GetBookingPieChartData());
        }

        [HttpGet("GetMemberAndBookingLineChart")]
        public async Task<ActionResult<ResponseDTO>> GetMemberAndBookingLineChart()
        {
            return Ok(await _chartSerice.GetMemberAndBookingLineChartData());
        }

        [HttpGet("GetRegisterUserChart")]
        public async Task<ActionResult<ResponseDTO>> GetRegisterUserChart()
        {
            return Ok(await _chartSerice.GetRegisteredUserChartData());
        }

        [HttpGet("GetRevenueChart")]
        public async Task<ActionResult<ResponseDTO>> GetRevenueChart()
        {
            return Ok(await _chartSerice.GetRevenueChartData());
        }

        [HttpGet("GetTotalBookingRadialChart")]
        public async Task<ActionResult<ResponseDTO>> GetTotalBookingRadialChart()
        {
            return Ok(await _chartSerice.GetTotalBookingRadialChartData());
        }
    }
}
