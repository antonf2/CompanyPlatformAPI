namespace CompanyAPI.DTOs
{
    public class UpdateInventoryItemDto
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string Location { get; set; }
    }
}