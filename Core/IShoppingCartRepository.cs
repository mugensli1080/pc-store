using System.Threading.Tasks;
using pc_store.Core.Models;

namespace pc_store.Core
{
    public interface IShoppingCartRepository
    {
        Task<ShoppingCart> GetShoppingCart(int id, bool includeRelated = true);
        void Add(ShoppingCart shoppingCart);
        void Remove(ShoppingCart shoppingCart);
    }
}