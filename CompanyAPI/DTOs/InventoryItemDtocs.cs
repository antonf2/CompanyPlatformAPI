using CompanyAPI.Data.Entities;

public class InventoryItemDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
    public string Location { get; set; }

    public InventoryItemDto() { }

    public InventoryItemDto(InventoryItem item)
    {
        if (item != null)
        {
            Id = item.ItemId;
            Name = item.Name;
            Description = item.Description;
            Quantity = item.Quantity;
            Location = item.Location;
        }
    }
}
