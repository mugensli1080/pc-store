using System;
using System.Collections.Generic;
using pc_store.Core.Models;

namespace pc_store.Controllers.Resources
{
    public class SaveProductResource
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public ICollection<ProductPhoto> ProductPhotos { get; set; }
        public ICollection<SpecificationPhoto> SpecificationPhotos { get; set; }
        public string VideoLink { get; set; }
    }
}