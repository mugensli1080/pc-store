using System.Collections.Generic;
using System.Threading.Tasks;
using pc_store.Core.Models;

namespace pc_store.Core
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategory(int id);
        void Add(Category category);
        void Remove(Category category);
    }
}