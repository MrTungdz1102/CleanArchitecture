using CleanArchitecture.WebUI.Models;
using CleanArchitecture.WebUI.Models.DTOs;
using CleanArchitecture.WebUI.Models.ViewModel;
using CleanArchitecture.WebUI.Services.Interfaces;
using CleanArchitecture.WebUI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Data;

namespace CleanArchitecture.WebUI.Controllers
{
    [Authorize(Roles = Constants.Role_Admin + "," + Constants.Role_Manager)]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        public UserController(IUserService userService, IRoleService roleService)
        {
            _roleService = roleService;
            _userService = userService;

        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Managment(string userId)
        {
            ResponseDTO? response = await _userService.GetUserInfoAsync(userId);
            ManageUserVM userVM = new();
            AppUser user = new();
            List<string>? roleList;
            List<SelectListItem>? roleItems = new List<SelectListItem>();
            if (response != null && response.IsSuccess)
            {
                user = JsonConvert.DeserializeObject<AppUser>(Convert.ToString(response.Result));
                response = await _roleService.GetAllRole();
                roleList = JsonConvert.DeserializeObject<List<string>>(Convert.ToString(response.Result));
                roleItems = roleList.Select(r => new SelectListItem { Value = r, Text = r }).ToList();
                userVM.User = user;
                userVM.RoleList = roleItems;
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(userVM);
        }

        [HttpPost]
        public async Task<IActionResult> Managment(ManageUserVM userVM)
        {
            ResponseDTO? response = await _userService.UpdateUserAsync(userVM.User);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Update user successfull!";
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(nameof(Index));
        }

        public async Task<IActionResult> GetAll()
        {
            ResponseDTO? response = await _userService.GetAllUserAsync();
            List<AppUser> userList = null;
            if (response!.Result != null && response.IsSuccess)
            {
                userList = JsonConvert.DeserializeObject<List<AppUser>>(Convert.ToString(response.Result));
                foreach (var item in userList)
                {
                    response = await _roleService.GetAllUserRoleAsync(item.Id);
                    item.Roles = JsonConvert.DeserializeObject<string[]>(Convert.ToString(response.Result));
                }
            }
            else
            {
                TempData["error"] = response?.Message;
                return RedirectToAction("Index", "Home");
            }
            return Json(new { data = userList });
        }

        public async Task<IActionResult> LockUnlock(string id)
        {
            ResponseDTO? response = await _userService.LockUnlockAsync(id);
            if (response.IsSuccess)
            {
                return Json(new { success = true, message = "Lock/Unlock Successful" });
            }
            else
            {
                return Json(new { success = false, message = "Error while locking/unlocking" });
            }
        }
    }
}
