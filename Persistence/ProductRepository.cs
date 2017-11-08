using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pc_store.Core;
using pc_store.Core.Models;

namespace pc_store.Persistence
{
    public class ProductRespository : IProductRepository
    {
        private readonly PcStoreDbContext _context;

        public ProductRespository(PcStoreDbContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var products = await _context.Products
                                                .Include(p => p.Category)
                                                .Include(p => p.SubCategory)
                                                .Include(p => p.ProductPhotos)
                                                .Include(p => p.SpecificationPhotos)
                                                .ToListAsync();
            return products;
        }

        public async Task<Product> GetProduct(int id, bool includeRelated = true)
        {
            if (!includeRelated) return await _context.Products.FindAsync(id);
            var product = await _context.Products
                                                .Include(p => p.Category)
                                                .Include(p => p.SubCategory)
                                                .Include(p => p.ProductPhotos)
                                                .Include(p => p.SpecificationPhotos)
                                                .SingleOrDefaultAsync(p => p.Id == id);
            return product;
        }

        public void Add(Product product)
        {
            _context.Products.Add(product);
        }

        public void Remove(Product product)
        {
            _context.Products.Remove(product);
        }
    }
}