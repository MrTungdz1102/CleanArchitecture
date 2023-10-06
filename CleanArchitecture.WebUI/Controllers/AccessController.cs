using CleanArchitecture.WebUI.Models.DTOs;
using CleanArchitecture.WebUI.Models.ViewModel;
using CleanArchitecture.WebUI.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CleanArchitecture.WebUI.Controllers
{
    public class AccessController : Controller
    {
        private readonly IAuthService _authService;
        public AccessController(IAuthService authService)
        {
            _authService = authService;
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
            if(response != null && response.IsSuccess)
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
        public IActionResult Register(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            RegisterVM registerVM = new()
            {
                //RoleList = _roleManager.Roles.Select(x => new SelectListItem
                //{
                //    Text = x.Name,
                //    Value = x.Name
                //}),
                RedirectUrl = returnUrl
            };
            return View(registerVM);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            ResponseDTO? response = await _authService.Register(registerVM);
            if(response != null && response.IsSuccess)
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
            }
            return View(registerVM);
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
