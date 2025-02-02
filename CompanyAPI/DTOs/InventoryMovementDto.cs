namespace CompanyAPI.DTOs
{
    public class InventoryMovementDto
    {
        public int MovementId { get; set; }
        public string ItemId { get; set; }
        public string UserId { get; set; }
        public string Action { get; set; }
        public int QuantityChanged { get; set; }
        public string? Notes { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
