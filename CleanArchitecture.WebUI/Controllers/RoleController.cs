using CleanArchitecture.WebUI.Models.DTOs;
using CleanArchitecture.WebUI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CleanArchitecture.WebUI.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        public RoleController(IUserService userService, IRoleService roleService)
        {
            _roleService = roleService;

        }

        public async Task<IActionResult> Index()
        {
            List<string>? roleList = null;
            ResponseDTO? response = await _roleService.GetAllRole();
            if (response != null && response.IsSuccess)
            {
                roleList = JsonConvert.DeserializeObject<List<string>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(roleList);
        }
    }
}
