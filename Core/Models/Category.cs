using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pc_store.Core.Models
{
    [Table("Categories")]
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public List<Product> Products { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}