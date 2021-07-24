using Homuai.Domain.Entity;
using Homuai.Domain.Repository.Token;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Homuai.Infrastructure.DataAccess.Repositories
{
    public class TokenRepository : ITokenWriteOnlyRepository, ITokenReadOnlyRepository
    {
        private readonly HomuaiContext _context;

        public TokenRepository(HomuaiContext context) => _context = context;

        public async Task Add(Token token)
        {
            var tokenDatabase = await _context.Tokens.FirstOrDefaultAsync(c => c.UserId == token.UserId);

            if(tokenDatabase == null)
                await _context.Tokens.AddAsync(token);
            else
            {
                tokenDatabase.Value = token.Value;
                _context.Update(tokenDatabase);
            }
        }

        public async Task<Token> GetByUserId(long userId)
        {
            return await _context.Tokens.AsNoTracking().FirstOrDefaultAsync(c => c.UserId == userId);
        }
    }
}
