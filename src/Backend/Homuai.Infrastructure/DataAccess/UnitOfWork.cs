using Homuai.Domain.Repository;
using System;
using System.Threading.Tasks;

namespace Homuai.Infrastructure.DataAccess
{
    public sealed class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly HomuaiContext _context;
        private bool _disposed;

        public UnitOfWork(HomuaiContext context) => _context = context;

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
                _context.Dispose();

            _disposed = true;
        }
    }
}
