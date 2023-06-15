using KrTrade.WebApp.Core.Interfaces;
using KrTrade.WebApp.Relational.Data;

namespace KrTrade.WebApp.Relational.Repositories
{
    public class RepositoryManager : IUnitOfWork
    {
        private readonly KrTradeDbContext _context;
        private readonly IInstrumentsRepository? _instrumentRepository;

        public RepositoryManager(KrTradeDbContext context)
        {
            _context = context;
        }

        IInstrumentsRepository IUnitOfWork.InstrumentsRepository => _instrumentRepository ?? new InstrumentsRepository(_context);

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
