using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pc_store.Core;
using pc_store.Core.Models;

namespace pc_store.Persistence
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly PcStoreDbContext _context;
        public CategoryRepository(PcStoreDbContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategory(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public void Add(Category category)
        {
            _context.Categories.Add(category);
        }

        public void Remove(Category category)
        {
            _context.Categories.Remove(category);
        }
    }
}