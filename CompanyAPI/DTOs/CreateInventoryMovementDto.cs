namespace CompanyAPI.DTOs
{
    public class CreateInventoryMovementDto
    {
        public string ItemId { get; set; } 
        public string UserId { get; set; }  
        public string Action { get; set; }
        public int QuantityChanged { get; set; }
        public string? Notes { get; set; }
    }

}
