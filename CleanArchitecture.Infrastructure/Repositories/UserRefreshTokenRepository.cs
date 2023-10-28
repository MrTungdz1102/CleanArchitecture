using CleanArchitecture.ApplicationCore.Entities;
using CleanArchitecture.ApplicationCore.Interfaces.Repositories;
using CleanArchitecture.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class UserRefreshTokenRepository : Repository<UserRefreshToken>, IUserRefreshTokenRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRefreshTokenRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task DisableAllRefreshToken(string userId, string tokenId)
        {
            // better updaterange
            await _context.UserRefreshTokens.Where(u => u.UserId == userId
               && u.JwtTokenId == tokenId).ExecuteUpdateAsync(x => x.SetProperty(y => y.IsValidRefreshToken, false));
            await _context.SaveChangesAsync();
        }
    }
}
