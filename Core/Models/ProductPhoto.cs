using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pc_store.Core.Models
{
    [Table("ProductPhotos")]
    public class ProductPhoto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Thumbnail { get; set; }
        public int ProductId { get; set; }

        public bool Activated { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}