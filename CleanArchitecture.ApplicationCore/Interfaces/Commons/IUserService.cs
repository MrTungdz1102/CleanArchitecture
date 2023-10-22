using CleanArchitecture.ApplicationCore.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.ApplicationCore.Interfaces.Commons
{
    public interface IUserService
    {
        Task<int> GetAllQuantityUser();
        Task<int> CountUserCreateByTime(DateTime? startDate, DateTime? endDate);
        Task<List<AppUserDTO>> GetAllUser();
    }
}
