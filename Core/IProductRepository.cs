using System.Collections.Generic;
using System.Threading.Tasks;
using pc_store.Core.Models;

namespace pc_store.Core
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(int id, bool includeRelated = true);
        void Add(Product product);
        void Remove(Product product);
    }
}