using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pc_store.Core.Models
{
    [Table("SubCategories")]
    public class SubCategory
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

    }
}