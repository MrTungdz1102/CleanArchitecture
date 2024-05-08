using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  //  [Authorize]
    public class ChartAPIController : ControllerBase
    {
        private readonly IChartService _chartSerice;

        public ChartAPIController(IChartService chartService)
        {
            _chartSerice = chartService;
        }

        [HttpGet("GetBookingPieChart")]
        public async Task<ActionResult<ResponseDTO>> GetBookingPieChart(string? ownerId)
        {
            return Ok(await _chartSerice.GetBookingPieChartData(ownerId));
        }

        [HttpGet("GetMemberAndBookingLineChart")]
        public async Task<ActionResult<ResponseDTO>> GetMemberAndBookingLineChart(string? ownerId)
        {
            return Ok(await _chartSerice.GetMemberAndBookingLineChartData(ownerId));
        }

        [HttpGet("GetRegisterUserChart")]
        public async Task<ActionResult<ResponseDTO>> GetRegisterUserChart()
        {
            return Ok(await _chartSerice.GetRegisteredUserChartData());
        }

        [HttpGet("GetRevenueChart")]
        public async Task<ActionResult<ResponseDTO>> GetRevenueChart(string? ownerId)
        {
            return Ok(await _chartSerice.GetRevenueChartData(ownerId));
        }

        [HttpGet("GetTotalBookingRadialChart")]
        public async Task<ActionResult<ResponseDTO>> GetTotalBookingRadialChart(string? ownerId)
        {
            return Ok(await _chartSerice.GetTotalBookingRadialChartData(ownerId));
        }
    }
}
