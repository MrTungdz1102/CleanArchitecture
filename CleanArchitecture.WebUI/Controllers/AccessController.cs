using CleanArchitecture.WebUI.Models;
using CleanArchitecture.WebUI.Models.DTOs;
using CleanArchitecture.WebUI.Models.ViewModel;
using CleanArchitecture.WebUI.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CleanArchitecture.WebUI.Controllers
{
    public class AccessController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IRoleService _roleService;
        private readonly ITokenProvider _token;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;

        public AccessController(IAuthService authService, IRoleService roleService, ITokenProvider token, IEmailService emailService, IUserService userService)
        {
            _authService = authService;
            _roleService = roleService;
            _token = token;
            _emailService = emailService;
            _userService = userService;
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
            if (response.Result != null && response.IsSuccess)
            {
                //   TempData["success"] = "Hello " + loginVM.Email;
                AppUserVM appUserVM = JsonConvert.DeserializeObject<AppUserVM>(response.Result.ToString());
                await SignInUser(appUserVM.Token, appUserVM.AppUser);
                _token.SetToken(appUserVM.Token);
                var userRoles = GetRolesFromToken(appUserVM.Token);
                if (userRoles != null && userRoles.Any(x => x == Utilities.Constants.Role_Manager || x == Utilities.Constants.Role_Admin || x == Utilities.Constants.Role_Customer))
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    if (string.IsNullOrEmpty(loginVM.RedirectUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return LocalRedirect(loginVM.RedirectUrl);
                    }
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
                EmailVM email = new EmailVM { Email = registerVM.Email, Subject = "Register Account", Message = "<p>New Account Created Successfull</p>" };
                response = await _emailService.SendEmailAsync(email);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Send Email Success ";
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
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

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM forgotPassword)
        {
            ResponseDTO? response = await _userService.ForgotPasswordAsync(forgotPassword.Email);
            if (response != null && response.IsSuccess)
            {
                string? url = JsonConvert.DeserializeObject<string>(Convert.ToString(response.Result));
                string? filePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "ResetPassword.html");

                string content = await System.IO.File.ReadAllTextAsync(filePath);

                content = content
                .Replace("{{reset_url}}", url)
                .Replace("{{name}}", forgotPassword.Email)
                .Replace("{{email}}", forgotPassword.Email);

                EmailVM emailVM = new EmailVM
                {
                    Email = forgotPassword.Email,
                    Subject = "Reset Password",
                    Message = content
                };
                response = await _emailService.SendEmailAsync(emailVM);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Your reset password request has been sent to your email!";
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ResetPassword(string email, string token)
        {
            ResetPasswordDTO resetPassword = new ResetPasswordDTO
            {
                Email = email,
                Token = token,
            };
            return View(resetPassword);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO resetPassword)
        {
            ResponseDTO? response = await _userService.ResetPasswordAsync(resetPassword);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Reset password success!";
                return RedirectToAction("Login", "Access");
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(resetPassword);
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO passwordDTO)
        {
            ResponseDTO? response = await _userService.ChangePasswordAsync(passwordDTO);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Change password success!";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["error"] = response?.Message;
                return View(passwordDTO);
            }
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            _token.ClearToken();
            return RedirectToAction("Index", "Home");
        }
        private async Task SignInUser(string token, AppUser appUser)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwt = handler.ReadJwtToken(token);
            ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email, jwt.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, jwt.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name, jwt.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Name).Value));


            identity.AddClaim(new Claim(ClaimTypes.Name, jwt.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Name).Value));
            var roles = jwt.Claims.Where(x => x.Type == "role").Select(x => x.Value);
            foreach (var role in roles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }
            // identity.AddClaim(new Claim(ClaimTypes.Role, jwt.Claims.FirstOrDefault(x => x.Type == "role").Value));
            identity.AddClaim(new Claim(ClaimTypes.Email, jwt.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, jwt.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub).Value));
            identity.AddClaim(new Claim(ClaimTypes.MobilePhone, appUser.PhoneNumber));
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }

        private List<string> GetRolesFromToken(string token)
        {
            List<string> roleList = new();
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwt = handler.ReadJwtToken(token);
            var roles = jwt.Claims.Where(x => x.Type == "role").Select(x => x.Value);
            roleList.AddRange(roles);
            return roleList;
        }
    }
}
