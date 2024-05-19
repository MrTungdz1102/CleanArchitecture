using CleanArchitecture.WebUI.Models;
using CleanArchitecture.WebUI.Models.DTOs;
using CleanArchitecture.WebUI.Services.Interfaces;
using CleanArchitecture.WebUI.Utilities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace CleanArchitecture.WebUI.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IChartService _chartService;

        public DashboardController(IChartService chartService)
        {
            _chartService = chartService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetMemberAndBookingLineChartData()
        {
            string? userId = null;
            if (User.IsInRole(Constants.Role_Customer))
            {
                userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            ResponseDTO? response = await _chartService.GetMemberAndBookingLineChart(userId);
            if (response is not null && response.IsSuccess)
            {
                LineChart lineChart = JsonConvert.DeserializeObject<LineChart>(response.Result.ToString());
                return Json(lineChart);
            }
            else
            {
                TempData["error"] = response?.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> GetBookingPieChartData()
        {
            string? userId = null;
            if (User.IsInRole(Constants.Role_Customer))
            {
                userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            ResponseDTO? response = await _chartService.GetBookingPieChart(userId);
            if (response is not null && response.IsSuccess)
            {
                PieChart pieChart = JsonConvert.DeserializeObject<PieChart>(response.Result.ToString());
                return Json(pieChart);
            }
            else
            {
                TempData["error"] = response?.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> GetTotalBookingRadialChartData()
        {
            string? userId = null;
            if (User.IsInRole(Constants.Role_Customer))
            {
                userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            ResponseDTO? response = await _chartService.GetTotalBookingRadialChart(userId);
            if (response is not null && response.IsSuccess)
            {
                RadialBarChart radialBarChart = JsonConvert.DeserializeObject<RadialBarChart>(response.Result.ToString());
                return Json(radialBarChart);
            }
            else
            {
                TempData["error"] = response?.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> GetRevenueChartData()
        {
            string? userId = null;
            if (User.IsInRole(Constants.Role_Customer))
            {
                userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            ResponseDTO? response = await _chartService.GetRevenueChart(userId);
            if (response is not null && response.IsSuccess)
            {
                RadialBarChart radialBarChart = JsonConvert.DeserializeObject<RadialBarChart>(response.Result.ToString());
                return Json(radialBarChart);
            }
            else
            {
                TempData["error"] = response?.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> GetRegisteredUserChartData()
        {
            ResponseDTO? response = await _chartService.GetRegisterUserChart();
            if (response is not null && response.IsSuccess)
            {
                RadialBarChart radialBarChart = JsonConvert.DeserializeObject<RadialBarChart>(response.Result.ToString());
                return Json(radialBarChart);
            }
            else
            {
                TempData["error"] = response?.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
