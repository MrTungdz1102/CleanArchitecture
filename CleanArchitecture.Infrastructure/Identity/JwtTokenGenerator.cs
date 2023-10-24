using Azure.Core;
using CleanArchitecture.ApplicationCore.Commons;
using CleanArchitecture.ApplicationCore.Interfaces.Commons;
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

        public async Task<string> CreateRefreshToken(string userName)
        {
            _user = await _userManager.FindByNameAsync(userName);
            if (_user == null) throw new UserNotFoundException(userName);
            await _userManager.RemoveAuthenticationTokenAsync(_user, _loginProvider, _refreshToken);
            var newRefreshToken = await _userManager.GenerateUserTokenAsync(_user, _loginProvider, _refreshToken);
            await _userManager.SetAuthenticationTokenAsync(_user, _loginProvider, _refreshToken, newRefreshToken);
            return newRefreshToken;
        }

        public async Task<string> GenerateToken(string userName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = Encoding.UTF8.GetBytes(_configuration["JWTSettings:Key"]);
            var credentials = new SigningCredentials(new SymmetricSecurityKey(securityKey), SecurityAlgorithms.HmacSha256Signature);
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) throw new UserNotFoundException(userName);
            var claimList = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Name, user.Name)
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

        public async Task<string> VerifyRefreshToken(LoginResponseDTO loginResponseDTO)
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
                return await GenerateToken(username);                
            }
            await _userManager.UpdateSecurityStampAsync(_user);
            return null;
        }
    }
}
