using System.Collections.Generic;
using pc_store.Core.Models;

namespace pc_store.Controllers.Resources
{
    public class SaveShoppingCartResource
    {
        public int Id { get; set; }
        public ICollection<CartItemResource> CartItems { get; set; }
    }
}