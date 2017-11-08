using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pc_store.Core;
using pc_store.Core.Models;

namespace pc_store.Persistence
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly PcStoreDbContext _context;
        public ShoppingCartRepository(PcStoreDbContext context)
        {
            this._context = context;

        }

        public async Task<ShoppingCart> GetShoppingCart(int id, bool includeRelated = true)
        {
            if (!includeRelated) return await _context.ShoppingCarts.FindAsync(id);

            var cart = await _context.ShoppingCarts
                                                    .Include(c => c.CartItems)
                                                    .SingleOrDefaultAsync(c => c.Id == id);
            return cart;
        }

        public void Add(ShoppingCart shoppingCart)
        {
            _context.ShoppingCarts.Add(shoppingCart);
        }

        public void Remove(ShoppingCart shoppingCart)
        {
            _context.ShoppingCarts.Remove(shoppingCart);
        }
    }
}