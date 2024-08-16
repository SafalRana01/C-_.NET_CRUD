using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace FirstCrudProject.Models.DBEntities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = "";

        [MaxLength(100)]
        public string Brand { get; set; } = "";

        [MaxLength(100)]
        public string Category { get; set; } = "";

        [MaxLength(100)]
        public string Description { get; set; } = "";

        [MaxLength(100)]
        public string ImageFileName { get; set; } = "";

        [Precision(16, 2)]
        public string Price { get; set; } = "";

        public DateTime CreatedAt { get; set; }
    }
}
