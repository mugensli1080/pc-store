using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pc_store.Core.Models
{
    [Table("Products")]
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }
        public ICollection<ProductPhoto> ProductPhotos { get; set; }
        public ICollection<SpecificationPhoto> SpecificationPhotos { get; set; }

        public string VideoLink { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public Product()
        {
            ProductPhotos = new Collection<ProductPhoto>();
            SpecificationPhotos = new Collection<SpecificationPhoto>();
        }

    }
}