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
        public async Task<ActionResult<ResponseDTO>> Login([FromBody] LoginRequestDTO loginRequest)
        {
            return Ok(await _authService.Login(loginRequest));
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ResponseDTO>> Register([FromBody] RegisterRequestDTO registerRequest)
        {
            return Ok(await _authService.Register(registerRequest));
        }

        [HttpPost("VerifyRefreshToken")]
     //   [Obsolete]
     // not complete
        public async Task<ActionResult<ResponseDTO>> VerifyRefreshToken([FromBody] LoginResponseDTO loginResponse)
        {
            return Ok(await _authService.VerifyRefreshToken(loginResponse));
        }

        [HttpPost("RefreshAccessToken")]
        public async Task<ActionResult<ResponseDTO>> RefreshAccessToken([FromBody] LoginResponseDTO loginResponseDTO)
        {
            return Ok(await _authService.RefreshAccessToken(loginResponseDTO));
        }
    }
}
