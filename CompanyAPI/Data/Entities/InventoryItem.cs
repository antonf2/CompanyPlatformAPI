using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyAPI.Data.Entities
{
    public class InventoryItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemId { get; set; } 
        [Required]
        [StringLength(255)]
        public required string Name { get; set; }

        public required string Description { get; set; }

        [Required]
        public int Quantity { get; set; }

        [StringLength(255)]
        public required string Location { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }

}
