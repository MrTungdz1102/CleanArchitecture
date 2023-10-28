using Azure.Core;
using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Interfaces.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Identity
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private AppUser? _user;
        private const string _loginProvider = "TungDaoAPI";
        private const string _refreshToken = "TungDaoRefreshToken";
        public JwtTokenGenerator(IConfiguration configuration, UserManager<AppUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<string> CreateRefreshToken(string userId)
        {
            _user = await _userManager.FindByIdAsync(userId);
            if (_user == null) throw new UserNotFoundException(userId);
            await _userManager.RemoveAuthenticationTokenAsync(_user, _loginProvider, _refreshToken);
            var newRefreshToken = await _userManager.GenerateUserTokenAsync(_user, _loginProvider, _refreshToken);
            await _userManager.SetAuthenticationTokenAsync(_user, _loginProvider, _refreshToken, newRefreshToken);
            return newRefreshToken;
        }

        public async Task<string> GenerateToken(string userId, string jwtTokenId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = Encoding.UTF8.GetBytes(_configuration["JWTSettings:Key"]);
            var credentials = new SigningCredentials(new SymmetricSecurityKey(securityKey), SecurityAlgorithms.HmacSha256Signature);
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) throw new UserNotFoundException(userId);
            var claimList = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, jwtTokenId),
                new Claim(JwtRegisteredClaimNames.Name, user.Name),
                new Claim("PhoneNumber", user.PhoneNumber)
            };
            var roles = await _userManager.GetRolesAsync(user);
            claimList.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _configuration["JWTSettings:Audience"],
                Issuer = _configuration["JWTSettings:Issuer"],
                // Claims = claims khi sử dụng JwtSecurityToken thay vì SecurityTokenDescriptor
                Subject = new ClaimsIdentity(claimList),
                Expires = DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JwtSettings:DurationInMinutes"])),
                SigningCredentials = credentials
            };
            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<LoginResponseDTO> VerifyRefreshToken(LoginResponseDTO loginResponseDTO)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(loginResponseDTO.Token);
            var username = tokenContent.Claims.ToList().FirstOrDefault(q => q.Type == JwtRegisteredClaimNames.Email)?.Value;
            _user = await _userManager.FindByNameAsync(username);

            if (_user == null || _user.Id != loginResponseDTO.Id)
            {
                return null;
            }

            var isValidRefreshToken = await _userManager.VerifyUserTokenAsync(_user, _loginProvider, _refreshToken, loginResponseDTO.RefreshToken);

            if (isValidRefreshToken)
            {
                var jwtTokenId = $"JTI{Guid.NewGuid()}";
                var token =  await GenerateToken(username, jwtTokenId);
                return new LoginResponseDTO
                {
                    Token = token,
                    Id = _user.Id,
                    RefreshToken = await CreateRefreshToken(loginResponseDTO.Email),
                    RefreshTokenExpire = DateTime.Now.AddMinutes(int.Parse(_configuration["RefreshToken:ExpiresDay"]))
                };
            }
            await _userManager.UpdateSecurityStampAsync(_user);
            return null;
        }

        public bool ValidateAccessToken(string accessToken, string expectedUserId, string expectedTokenId)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwt = tokenHandler.ReadJwtToken(accessToken);
                var jwtTokenId = jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Jti).Value;
                var userId = jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value;
                return userId == expectedUserId && jwtTokenId == expectedTokenId;
            }
            catch
            {
                return false;
            }
        }
    }
}
