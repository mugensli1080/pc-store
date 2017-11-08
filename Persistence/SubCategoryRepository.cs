
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pc_store.Core;
using pc_store.Core.Models;

namespace pc_store.Persistence
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly PcStoreDbContext _context;
        public SubCategoryRepository(PcStoreDbContext context)
        {
            this._context = context;

        }

        public async Task<IEnumerable<SubCategory>> GetSubCategories(int categoryId)
        {
            return await _context.SubCategories
                                                .Where(sc => sc.CategoryId == categoryId)
                                                .ToListAsync();
        }

        public async Task<SubCategory> GetSubCategory(int id)
        {
            return await _context.SubCategories.FindAsync(id);
        }

        public void Add(SubCategory subCategory)
        {
            _context.SubCategories.Add(subCategory);
        }

        public void Remove(SubCategory subCategory)
        {
            _context.SubCategories.Remove(subCategory);
        }
    }
}