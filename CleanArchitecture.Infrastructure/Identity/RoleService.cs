using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Interfaces.Commons;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Identity
{
    public class RoleService : IRoleService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private ResponseDTO _response;
        public RoleService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _response = new ResponseDTO();

        }
        public async Task<bool> AssignRole(string email, string[] roleName)
        {
            AppUser? user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                foreach (var role in roleName)
                {
                    if (!await _roleManager.RoleExistsAsync(role))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(role));
                    }
                    if (role is not null)
                    {
                        await _userManager.AddToRoleAsync(user, role);
                    }
                }

                return true;
            }
            return false;
        }

        public ResponseDTO GetAllRole()
        {
            try
            {
                _response.Result = _roleManager.Roles.Select(x => x.Name).ToList();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
