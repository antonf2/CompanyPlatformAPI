namespace CompanyAPI.DTOs
{
    public class UpdateInventoryItemDto
    {
        public int Id { get; set; } // Include to validate against URL
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Location { get; set; }
    }

}
