using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Entities;
using CleanArchitecture.ApplicationCore.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.ApplicationCore.Interfaces.Services
{
    public interface IAuthService
    {
        Task<ResponseDTO> Register(RegisterRequestDTO registerRequest);
        Task<ResponseDTO> Login(LoginRequestDTO loginRequest);
        Task<ResponseDTO> VerifyRefreshToken(LoginResponseDTO loginResponseDTO);
        Task<string> CreateUserRefreshToken(string userId, string jwtTokenId);
        Task<ResponseDTO> RefreshAccessToken(TokenDTO tokenDTO);
    }
}
