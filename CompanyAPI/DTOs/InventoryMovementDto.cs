namespace CompanyAPI.DTOs
{
    public class InventoryMovementDto
    {
        public int MovementId { get; set; }
        public int ItemId { get; set; }
        public int UserId { get; set; }
        public string Action { get; set; }
        public int QuantityChanged { get; set; }
        public string? Notes { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
