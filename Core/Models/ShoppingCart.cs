using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace pc_store.Core.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public ShoppingCart()
        {
            CartItems = new Collection<CartItem>();
        }
    }
}