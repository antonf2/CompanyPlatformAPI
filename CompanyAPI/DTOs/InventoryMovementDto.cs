using System.ComponentModel.DataAnnotations;

namespace CompanyAPI.DTOs
{
    public class InventoryMovementDto
    {
        public int MovementId { get; set; }
        [Required]
        public required string ItemId { get; set; }
        [Required]
        public required string UserId { get; set; }
        [Required]
        public required string Action { get; set; }
        public int QuantityChanged { get; set; }
        public string? Notes { get; set; }
        public DateTime Timestamp { get; set; }
    }

}
