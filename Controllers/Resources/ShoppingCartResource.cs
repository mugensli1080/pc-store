using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using pc_store.Core.Models;

namespace pc_store.Controllers.Resources
{
    public class ShoppingCartResource
    {
        public int Id { get; set; }
        public ICollection<CartItemResource> CartItems { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public ShoppingCartResource()
        {
            CartItems = new Collection<CartItemResource>();
        }
    }
}