using System.Collections.Generic;
using System.Threading.Tasks;
using pc_store.Core.Models;

namespace pc_store.Core
{
    public interface ISubCategoryRepository
    {
        Task<IEnumerable<SubCategory>> GetSubCategories(int categoryId);
        Task<SubCategory> GetSubCategory(int id);
        void Add(SubCategory subCategory);
        void Remove(SubCategory subCategory);
    }
}