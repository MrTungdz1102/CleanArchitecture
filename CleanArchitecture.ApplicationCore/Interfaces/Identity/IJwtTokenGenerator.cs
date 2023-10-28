using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.ApplicationCore.Interfaces.Identity
{
    public interface IJwtTokenGenerator
    {
        Task<string> GenerateToken(string userName, string jwtTokenId);
        Task<string> CreateRefreshToken(string userId);
        Task<LoginResponseDTO> VerifyRefreshToken(LoginResponseDTO loginResponseDTO);
        bool ValidateAccessToken(string accessToken, string expectedUserId, string expectedTokenId);
    }
}
