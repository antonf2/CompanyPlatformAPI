using System.ComponentModel.DataAnnotations;

namespace CompanyAPI.DTOs
{
    public class UpdateInventoryItemDto
    {
        public int ItemId { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Description { get; set; }
        public int Quantity { get; set; }
        [Required]
        public required string Location { get; set; }
    }
}