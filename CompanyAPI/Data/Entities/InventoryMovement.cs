using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CompanyAPI.Data.Entities
{
    public class InventoryMovement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MovementId { get; set; }

        [Required]
        public int ItemId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Action { get; set; }

        [Required]
        public int QuantityChanged { get; set; }

        public string? Notes { get; set; }

        [Required]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        [ForeignKey("ItemId")]
        public InventoryItem InventoryItem { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}