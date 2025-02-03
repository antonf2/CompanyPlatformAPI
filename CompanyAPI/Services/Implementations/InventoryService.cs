using CompanyAPI.Data;
using CompanyAPI.Data.Entities;
using CompanyAPI.DTOs;
using CompanyAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CompanyAPI.Services.Implementations
{
    public class InventoryService : IInventoryService
    {
        private readonly ContextDAL _context;

        public InventoryService(ContextDAL context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InventoryItemDto>> GetAllItemsAsync()
        {
            var items = await _context.InventoryItems.ToListAsync();
            return items.Select(item => new InventoryItemDto
            {
                ItemId = item.ItemId,
                Name = item.Name,
                Description = item.Description,
                Quantity = item.Quantity,
                Location = item.Location,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
        }

        public async Task<InventoryItemDto> GetItemByIdAsync(int id)
        {
            var item = await _context.InventoryItems.FirstOrDefaultAsync(i => i.ItemId == id);
            if (item == null)
                throw new KeyNotFoundException($"Inventory item with ID {id} not found.");

            return new InventoryItemDto
            {
                ItemId = item.ItemId,
                Name = item.Name,
                Description = item.Description,
                Quantity = item.Quantity,
                Location = item.Location,
                CreatedAt = item.CreatedAt,
                UpdatedAt = item.UpdatedAt ?? DateTime.MinValue
            };
        }

        public async Task<InventoryItemDto> CreateItemAsync(CreateInventoryItemDto itemDto)
        {
            var item = new InventoryItem
            {
                Name = itemDto.Name,
                Description = itemDto.Description,
                Quantity = itemDto.Quantity,
                Location = itemDto.Location,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.InventoryItems.Add(item);
            await _context.SaveChangesAsync();

            return new InventoryItemDto
            {
                ItemId = item.ItemId,
                Name = item.Name,
                Description = item.Description,
                Quantity = item.Quantity,
                Location = item.Location,
                CreatedAt = item.CreatedAt,
                UpdatedAt = item.UpdatedAt ?? DateTime.MinValue
            };
        }

        public async Task<bool> UpdateItemAsync(int id, UpdateInventoryItemDto itemDto)
        {
            var item = await _context.InventoryItems.FirstOrDefaultAsync(i => i.ItemId == id);

            if (item == null)
                return false;

            item.Name = itemDto.Name;
            item.Description = itemDto.Description;
            item.Quantity = itemDto.Quantity;
            item.Location = itemDto.Location;
            item.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var item = await _context.InventoryItems.FirstOrDefaultAsync(i => i.ItemId == id);

            if (item == null)
                return false;

            _context.InventoryItems.Remove(item);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}