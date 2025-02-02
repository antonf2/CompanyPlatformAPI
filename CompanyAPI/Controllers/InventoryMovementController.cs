using CompanyAPI.DTOs;
using CompanyAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CompanyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryMovementController : ControllerBase
    {
        private readonly IInventoryMovementService _movementService;

        public InventoryMovementController(IInventoryMovementService movementService)
        {
            _movementService = movementService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMovements()
        {
            var movements = await _movementService.GetAllMovementsAsync();
            return Ok(movements);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovementById(int id)
        {
            var movement = await _movementService.GetMovementByIdAsync(id);
            if (movement == null) return NotFound();
            return Ok(movement);
        }

        [HttpPost]
        public async Task<IActionResult> LogMovement([FromBody] CreateInventoryMovementDto movementDto)
        {
            var movement = await _movementService.LogMovementAsync(movementDto);
            return CreatedAtAction(nameof(GetMovementById), new { id = movement.MovementId }, movement);
        }
    }
}

