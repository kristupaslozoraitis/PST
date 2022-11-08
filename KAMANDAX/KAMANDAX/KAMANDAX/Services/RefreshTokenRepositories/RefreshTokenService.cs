using KAMANDAX.DB;
using KAMANDAX.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KAMANDAX.Services.RefreshTokenRepositories
{
    public class RefreshTokenService
    {
        private readonly WebDbContext _db;

        public RefreshTokenService(WebDbContext db)
        {
            _db = db;
        }

        public async Task Create(RefreshToken refreshToken)
        {
            refreshToken.Id = new Guid();
            _db.RefreshTokens.Add(refreshToken);
            await _db.SaveChangesAsync();
        }
        public async Task Delete(Guid id)
        {
            RefreshToken refreshToken = await _db.RefreshTokens.FindAsync(id);
            if(refreshToken != null)
            {
                _db.RefreshTokens.Remove(refreshToken);
                await _db.SaveChangesAsync();
            }
        }
        public async Task DeleteAll(Guid userId)
        {
            IEnumerable<RefreshToken> refreshTokens = await _db.RefreshTokens.Where(t => t.UserId == userId).ToListAsync();

            _db.RefreshTokens.RemoveRange(refreshTokens);
            await _db.SaveChangesAsync();
        }
        public async Task<RefreshToken> GetByToken(string token)
        {
            return await _db.RefreshTokens.FirstOrDefaultAsync(t => t.Token == token);
        }
        public async Task<RefreshToken> GetByUserId(Guid id)
        {
            return await _db.RefreshTokens.FirstOrDefaultAsync(t => t.UserId == id);
        }
    }
}
