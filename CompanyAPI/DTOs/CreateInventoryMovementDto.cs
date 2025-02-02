namespace CompanyAPI.DTOs
{
    public class CreateInventoryMovementDto
    {
        public int ItemId { get; set; }
        public int UserId { get; set; }
        public string Action { get; set; }
        public int QuantityChanged { get; set; }
        public string? Notes { get; set; }
    }
}
