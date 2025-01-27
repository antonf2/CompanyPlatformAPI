using CompanyAPI.DTOs;
using CompanyAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllItems()
        {
            var items = await _inventoryService.GetAllItemsAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemById(int id)
        {
            var item = await _inventoryService.GetItemByIdAsync(id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem([FromBody] CreateInventoryItemDto itemDto)
        {
            var item = await _inventoryService.CreateItemAsync(itemDto);
            return CreatedAtAction(nameof(GetItemById), new { id = item.ItemId }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, [FromBody] UpdateInventoryItemDto itemDto)
        {
            var item = await _inventoryService.UpdateItemAsync(id, itemDto);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var result = await _inventoryService.DeleteItemAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}