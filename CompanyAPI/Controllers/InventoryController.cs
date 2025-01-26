using CompanyAPI.DTOs;
using CompanyAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<IEnumerable<InventoryItemDto>>> GetAllItems()
    {
        var items = await _inventoryService.GetAllItemsAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<InventoryItemDto>> GetItem(int id)
    {
        var itemDto = await _inventoryService.GetItemByIdAsync(id);
        if (itemDto == null)
            return NotFound($"Item with ID {id} not found.");
        return Ok(itemDto);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<InventoryItemDto>> CreateItem([FromBody] CreateInventoryItemDto itemDto)
    {
        var createdItem = await _inventoryService.CreateItemAsync(itemDto);
        return CreatedAtAction(nameof(GetItem), new { id = createdItem.Id }, createdItem);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateItem(int id, [FromBody] UpdateInventoryItemDto itemDto)
    {
        if (id != itemDto.Id)
            return BadRequest("ID mismatch in the URL and body.");

        var success = await _inventoryService.UpdateItemAsync(id, itemDto);
        if (!success)
            return NotFound($"Item with ID {id} not found.");

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteItem(int id)
    {
        var success = await _inventoryService.DeleteItemAsync(id);
        if (!success)
            return NotFound($"Item with ID {id} not found.");

        return NoContent();
    }
}
