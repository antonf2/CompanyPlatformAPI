using CompanyAPI.Data.Entities;
using CompanyAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryItem>>> GetAllItems()
        {
            var items = await _inventoryService.GetAllItemsAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryItem>> GetItem(int id)
        {
            var item = await _inventoryService.GetItemByIdAsync(id);
            if (item == null)
                return NotFound($"Item with ID {id} not found.");
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<InventoryItem>> CreateItem([FromBody] InventoryItem item)
        {
            var createdItem = await _inventoryService.CreateItemAsync(item);
            return CreatedAtAction(nameof(GetItem), new { id = createdItem.Id }, createdItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, [FromBody] InventoryItem item)
        {
            if (id != item.Id)
                return BadRequest("ID mismatch in the URL and body.");

            await _inventoryService.UpdateItemAsync(item);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // Restrict deletion to Admins only
        public async Task<IActionResult> DeleteItem(int id)
        {
            await _inventoryService.DeleteItemAsync(id);
            return NoContent();
        }
    }
}
