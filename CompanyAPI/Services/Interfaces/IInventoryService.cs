using CompanyAPI.Data.Entities;

namespace CompanyAPI.Services.Interfaces
{
    public interface IInventoryService
    {
        Task<IEnumerable<InventoryItem>> GetAllItemsAsync();
        Task<InventoryItem> GetItemByIdAsync(int id);
        Task<InventoryItem> CreateItemAsync(InventoryItem item);
        Task UpdateItemAsync(InventoryItem item);
        Task DeleteItemAsync(int id);
    }
}
