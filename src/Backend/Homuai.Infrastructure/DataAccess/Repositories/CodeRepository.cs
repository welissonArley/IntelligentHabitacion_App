using Homuai.Domain.Entity;
using Homuai.Domain.Repository.Code;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Homuai.Infrastructure.DataAccess.Repositories
{
    public class CodeRepository : ICodeWriteOnlyRepository, ICodeReadOnlyRepository
    {
        private readonly HomuaiContext _context;

        public CodeRepository(HomuaiContext context) => _context = context;

        public async Task Add(Code code)
        {
            DeleteAll(code.UserId);

            await _context.Codes.AddAsync(code);
        }

        public void DeleteAllFromTheUser(long userId)
        {
            DeleteAll(userId);
        }

        public async Task<Code> GetByCode(string code)
        {
            return await _context.Codes.AsNoTracking().FirstOrDefaultAsync(c => c.Value.ToUpper().Equals(code.ToUpper()));
        }

        public async Task<Code> GetByUserId(long userId)
        {
            return await _context.Codes.AsNoTracking().FirstOrDefaultAsync(c => c.UserId == userId && c.Active);
        }

        private void DeleteAll(long userId)
        {
            var codes = _context.Codes.Where(c => c.UserId == userId);

            if(codes.Any())
                _context.Codes.RemoveRange(codes);
        }
    }
}
