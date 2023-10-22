using AutoMapper;
using CleanArchitecture.ApplicationCore.Entities.DTOs;
using CleanArchitecture.ApplicationCore.Interfaces.Commons;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Identity
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public UserService(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<int> CountUserCreateByTime(DateTime? startDate, DateTime? endDate)
        {
            List<AppUser> appUsers = await _userManager.Users.ToListAsync();
            return appUsers.Count(x => x.CreateTime >= startDate && x.CreateTime <= endDate);
        }

        public async Task<int> GetAllQuantityUser()
        {
            List<AppUser> appUsers = await _userManager.Users.ToListAsync();
            return appUsers.Count;
        }
        public async Task<List<AppUserDTO>> GetAllUser()
        {
            IEnumerable<AppUser> appUsers =  await _userManager.Users.ToListAsync();
            List<AppUserDTO> appUserDTOs = new List<AppUserDTO>();
            foreach (var user in appUsers)
            {
                appUserDTOs.Add(AppUserMapper(user));
            }
            return appUserDTOs;
        }

        public AppUserDTO AppUserMapper(AppUser user)
        {
            return new AppUserDTO { Name = user.Name, CreateTime = user.CreateTime };
        }
    }
}
