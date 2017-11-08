using System;
using System.Collections.Generic;
using pc_store.Core.Models;

namespace pc_store.Controllers.Resources
{
    public class ProductResource : KeyValuePairResource
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public CategoryResource Category { get; set; }
        public SubCategoryResource SubCategory { get; set; }
        public ICollection<ProductPhoto> ProductPhotos { get; set; }
        public ICollection<SpecificationPhoto> SpecificationPhotos { get; set; }
        public string VideoLink { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}