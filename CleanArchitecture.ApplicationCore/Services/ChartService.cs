using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Entities;
using CleanArchitecture.ApplicationCore.Entities.DTOs;
using CleanArchitecture.ApplicationCore.Interfaces.Commons;
using CleanArchitecture.ApplicationCore.Interfaces.Repositories;
using CleanArchitecture.ApplicationCore.Interfaces.Services;
using CleanArchitecture.ApplicationCore.Specifications;
using System.Collections;

namespace CleanArchitecture.ApplicationCore.Services
{
    public class ChartService : IChartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private ResponseDTO _response;
        private readonly IUserService _userService;
        private static int previousMonth = DateTime.Now.Month == 1 ? 12 : DateTime.Now.Month - 1;
        private readonly DateTime previousMonthStartDate = new(DateTime.Now.Year, previousMonth, 1);
        private readonly DateTime currentMonthStartDate = new(DateTime.Now.Year, DateTime.Now.Month, 1);
        public ChartService(IUnitOfWork unitOfWork, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _response = new ResponseDTO();
            _userService = userService;
        }
        public async Task<ResponseDTO> GetBookingPieChartData()
        {
            try
            {
                var specification = new BookingFilterSpecification(DateTime.Now, PaymentStatus.StatusPending, PaymentStatus.StatusCancelled);
                List<Booking> bookings = await _unitOfWork.bookingRepo.ListAsync(specification);
                List<string> customerWithOneBooking = bookings.GroupBy(b => b.UserId).Where(x => x.Count() == 1).Select(x => x.Key).ToList();
                int bookingsByNewCustomer = customerWithOneBooking.Count();
                int bookingsByReturningCustomer = bookings.Count() - bookingsByNewCustomer;
                PieChart pieChart = new()
                {
                    Labels = new string[] { "New Customer Bookings", "Returning Customer Bookings" },
                    Series = new decimal[] { bookingsByNewCustomer, bookingsByReturningCustomer }
                };
                _response.Result = pieChart;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> GetMemberAndBookingLineChartData()
        {
            try
            {
                var specification = new BookingFilterSpecification(DateTime.Now.AddDays(-30), DateTime.Now);
                List<Booking> bookings = await _unitOfWork.bookingRepo.ListAsync(specification);
                var bookingData = bookings.GroupBy(b => b.BookingDate.Date).Select(u => new
                {
                    DateTime = u.Key,
                    NewBookingCount = u.Count()
                });

                List<AppUserDTO> appUserDTOs = await _userService.GetAllUser();
                var customerData = appUserDTOs.Where(u => u.CreateTime >= DateTime.Now.AddDays(-30) &&
            u.CreateTime.Date <= DateTime.Now).GroupBy(b => b.CreateTime.Date)
                .Select(u => new
                {
                    DateTime = u.Key,
                    NewCustomerCount = u.Count()
                });

                var leftJoin = bookingData.GroupJoin(customerData, booking => booking.DateTime, customer => customer.DateTime,
                    (booking, customer) => new
                    {
                        booking.DateTime,
                        booking.NewBookingCount,
                        NewCustomerCount = customer.Select(x => x.NewCustomerCount).FirstOrDefault()
                    });

                var rightJoin = customerData.GroupJoin(bookingData, customer => customer.DateTime, booking => booking.DateTime,
                (customer, booking) => new
                {
                    customer.DateTime,
                    NewBookingCount = booking.Select(x => x.NewBookingCount).FirstOrDefault(),
                    customer.NewCustomerCount
                });

                var mergedData = leftJoin.Union(rightJoin).OrderBy(x => x.DateTime).ToList();

                var newBookingData = mergedData.Select(x => x.NewBookingCount).ToArray();
                var newCustomerData = mergedData.Select(x => x.NewCustomerCount).ToArray();
                var categories = mergedData.Select(x => x.DateTime.ToString("MM/dd/yyyy")).ToArray();

                List<ChartData> chartDataList = new()
            {
                new ChartData
                {
                    Name = "New Bookings",
                    Data = newBookingData
                },
                new ChartData
                {
                    Name = "New Members",
                    Data = newCustomerData
                },
            };

                LineChart lineChart = new()
                {
                    Categories = categories,
                    Series = chartDataList
                };
                _response.Result = lineChart;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> GetRegisteredUserChartData()
        {
            try
            {
                int totalUsers = await _userService.GetAllQuantityUser();
                int countByCurrentMonth = await _userService.CountUserCreateByTime(currentMonthStartDate, DateTime.Now);
                int countByPreviousMonth = await _userService.CountUserCreateByTime(previousMonthStartDate, currentMonthStartDate);
                _response.Result = GetRadialCartDataModel(totalUsers, countByCurrentMonth, countByPreviousMonth);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> GetRevenueChartData()
        {
            try
            {
                var specification = new BookingFilterSpecification(PaymentStatus.StatusPending, PaymentStatus.StatusCancelled);
                List<Booking> bookings = await _unitOfWork.bookingRepo.ListAsync(specification);
                var totalRevenue = Convert.ToInt32(bookings.Sum(u => u.TotalCost));
                var countByCurrentMonth = bookings.Where(u => u.BookingDate >= currentMonthStartDate &&
               u.BookingDate <= DateTime.Now).Sum(u => u.TotalCost);

                var countByPreviousMonth = bookings.Where(u => u.BookingDate >= previousMonthStartDate &&
                u.BookingDate <= currentMonthStartDate).Sum(u => u.TotalCost);
                _response.Result = GetRadialCartDataModel(totalRevenue, countByCurrentMonth, countByPreviousMonth);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> GetTotalBookingRadialChartData()
        {
            try
            {
                var specification = new BookingFilterSpecification(PaymentStatus.StatusPending, PaymentStatus.StatusCancelled);
                List<Booking> bookings = await _unitOfWork.bookingRepo.ListAsync(specification);

                var countByCurrentMonth = bookings.Count(x => x.BookingDate >= currentMonthStartDate &&
                x.BookingDate <= DateTime.Now);
                var countByPreviousMonth = bookings.Count(u => u.BookingDate >= previousMonthStartDate &&
              u.BookingDate <= currentMonthStartDate);
                _response.Result = GetRadialCartDataModel(bookings.Count, countByCurrentMonth, countByPreviousMonth);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public RadialBarChart GetRadialCartDataModel(int totalCount, double currentMonthCount, double prevMonthCount)
        {
            RadialBarChart chart = new RadialBarChart();
            int increaseDecreaseRatio = 100;

            if (prevMonthCount != 0)
            {
                increaseDecreaseRatio = Convert.ToInt32((currentMonthCount - prevMonthCount) / prevMonthCount * 100);
            }
            chart.TotalCount = totalCount;
            chart.CountInCurrentMonth = Convert.ToInt32(currentMonthCount);
            chart.HasRatioIncreased = currentMonthCount > prevMonthCount;
            chart.Series = new int[] { increaseDecreaseRatio };
            return chart;
        }
    }
}
