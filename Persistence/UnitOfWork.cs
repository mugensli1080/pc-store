using System.Threading.Tasks;
using pc_store.Core;

namespace pc_store.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PcStoreDbContext _context;
        public UnitOfWork(PcStoreDbContext context)
        {
            this._context = context;

        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}