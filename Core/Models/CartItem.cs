using System;
using System.ComponentModel.DataAnnotations;

namespace pc_store.Core.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
        public int ShoppingCartId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}