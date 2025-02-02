using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class InventoryMovement
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MovementId { get; set; }

    [Required]
    [StringLength(50)]
    public string ItemId { get; set; }

    [Required]
    [StringLength(50)]
    public string UserId { get; set; } 

    [Required]
    [StringLength(50)]
    public string Action { get; set; }

    [Required]
    public int QuantityChanged { get; set; }

    public string? Notes { get; set; }

    [Required]
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
