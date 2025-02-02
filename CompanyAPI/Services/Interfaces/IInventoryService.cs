using CompanyAPI.DTOs;

namespace CompanyAPI.Services.Interfaces
{
    public interface IInventoryService
    {
        Task<IEnumerable<InventoryItemDto>> GetAllItemsAsync();
        Task<InventoryItemDto> GetItemByIdAsync(int id);
        Task<InventoryItemDto> CreateItemAsync(CreateInventoryItemDto itemDto);
        Task<bool> UpdateItemAsync(int id, UpdateInventoryItemDto itemDto);
        Task<bool> DeleteItemAsync(int id);
    }
}
