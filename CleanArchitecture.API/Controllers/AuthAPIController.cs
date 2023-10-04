using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthAPIController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ResponseDTO>> Login(LoginRequestDTO loginRequest)
        {
            return Ok(await _authService.Login(loginRequest));
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ResponseDTO>> Register(RegisterRequestDTO registerRequest)
        {
            return Ok(await _authService.Register(registerRequest));
        }
    }
}
