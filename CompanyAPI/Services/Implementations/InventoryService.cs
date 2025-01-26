using CompanyAPI.Data;
using CompanyAPI.Data.Entities;
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

        public async Task<IEnumerable<InventoryItem>> GetAllItemsAsync() =>
            await _context.InventoryItems.ToListAsync();

        public async Task<InventoryItem> GetItemByIdAsync(int id) =>
            await _context.InventoryItems.FirstOrDefaultAsync(item => item.Id == id);

        public async Task<InventoryItem> CreateItemAsync(InventoryItem item)
        {
            _context.InventoryItems.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task UpdateItemAsync(InventoryItem item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteItemAsync(int id)
        {
            var item = await _context.InventoryItems.FindAsync(id);
            if (item != null)
            {
                _context.InventoryItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
