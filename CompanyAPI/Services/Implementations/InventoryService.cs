using CompanyAPI.Data.Entities;
using CompanyAPI.Data;
using CompanyAPI.DTOs;
using CompanyAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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
        return items.Select(i => new InventoryItemDto(i));
    }

    public async Task<InventoryItemDto> GetItemByIdAsync(int id)
    {
        var item = await _context.InventoryItems.FindAsync(id);
        return item == null ? null : new InventoryItemDto(item);
    }

    public async Task<InventoryItemDto> CreateItemAsync(CreateInventoryItemDto itemDto)
    {
        var item = new InventoryItem
        {
            Name = itemDto.Name,
            Description = itemDto.Description,
            Quantity = itemDto.Quantity,
            Location = itemDto.Location
        };
        _context.InventoryItems.Add(item);
        await _context.SaveChangesAsync();
        return new InventoryItemDto(item);
    }

    public async Task<bool> UpdateItemAsync(int id, UpdateInventoryItemDto itemDto)
    {
        var item = await _context.InventoryItems.FindAsync(id);
        if (item == null) return false;

        item.Name = itemDto.Name;
        item.Description = itemDto.Description;
        item.Quantity = itemDto.Quantity;
        item.Location = itemDto.Location;

        _context.InventoryItems.Update(item);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteItemAsync(int id)
    {
        var item = await _context.InventoryItems.FindAsync(id);
        if (item == null) return false;

        _context.InventoryItems.Remove(item);
        await _context.SaveChangesAsync();
        return true;
    }
}
