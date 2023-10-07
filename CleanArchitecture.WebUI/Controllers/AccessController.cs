using CleanArchitecture.WebUI.Models.DTOs;
using CleanArchitecture.WebUI.Models.ViewModel;
using CleanArchitecture.WebUI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Data;

namespace CleanArchitecture.WebUI.Controllers
{
    public class AccessController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IRoleService _roleService;
        public AccessController(IAuthService authService, IRoleService roleService)
        {
            _authService = authService;
            _roleService = roleService;
        }
        public IActionResult Login(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            LoginVM loginVM = new()
            {
                RedirectUrl = returnUrl
            };
            return View(loginVM);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            ResponseDTO? response = await _authService.Login(loginVM);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Hello " + loginVM.Email;
                if (string.IsNullOrEmpty(loginVM.RedirectUrl))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return LocalRedirect(loginVM.RedirectUrl);
                }
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(loginVM);
        }
        public async Task<IActionResult> Register(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            ResponseDTO? response = await _roleService.GetAllRole();
            List<string>? roleList;
            List<SelectListItem>? roleItems = new List<SelectListItem>();
            if (response != null && response.IsSuccess)
            {
                roleList = JsonConvert.DeserializeObject<List<string>>(Convert.ToString(response.Result));
                roleItems = roleList.Select(r => new SelectListItem { Value = r, Text = r }).ToList();
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            RegisterVM registerVM = new()
            {
                RedirectUrl = returnUrl,
                RoleList = roleItems
            };

            return View(registerVM);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            ResponseDTO? response = await _authService.Register(registerVM);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Register Success " + registerVM.Email;
                if (string.IsNullOrEmpty(registerVM.RedirectUrl))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return LocalRedirect(registerVM.RedirectUrl);
                }

            }
            else
            {
                TempData["error"] = response?.Message;
                response = await _roleService.GetAllRole();
                List<string>? roleList;
                List<SelectListItem>? roleItems = new List<SelectListItem>();
                if (response != null && response.IsSuccess)
                {
                    roleList = JsonConvert.DeserializeObject<List<string>>(Convert.ToString(response.Result));
                    roleItems = roleList.Select(r => new SelectListItem { Value = r, Text = r }).ToList();
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
                registerVM.RoleList = roleItems;
            }            
            return View(registerVM);
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
