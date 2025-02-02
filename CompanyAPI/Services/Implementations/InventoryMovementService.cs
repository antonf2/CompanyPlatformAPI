using CompanyAPI.Data.Entities;
using CompanyAPI.Data;
using CompanyAPI.DTOs;
using CompanyAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CompanyAPI.Services.Implementations
{
    public class InventoryMovementService : IInventoryMovementService
    {
        private readonly ContextDAL _context;

        public InventoryMovementService(ContextDAL context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InventoryMovementDto>> GetAllMovementsAsync()
        {
            var movements = await _context.InventoryMovements.ToListAsync();
            return movements.Select(m => new InventoryMovementDto
            {
                MovementId = m.MovementId,
                ItemId = m.ItemId,
                UserId = m.UserId,
                Action = m.Action,
                QuantityChanged = m.QuantityChanged,
                Notes = m.Notes,
                Timestamp = m.Timestamp
            });
        }

        public async Task<InventoryMovementDto> GetMovementByIdAsync(int id)
        {
            var movement = await _context.InventoryMovements.FindAsync(id);
            if (movement == null) return null;

            return new InventoryMovementDto
            {
                MovementId = movement.MovementId,
                ItemId = movement.ItemId,
                UserId = movement.UserId,
                Action = movement.Action,
                QuantityChanged = movement.QuantityChanged,
                Notes = movement.Notes,
                Timestamp = movement.Timestamp
            };
        }

        public async Task<InventoryMovementDto> LogMovementAsync(CreateInventoryMovementDto movementDto)
        {
            var movement = new InventoryMovement
            {
                ItemId = movementDto.ItemId,
                UserId = movementDto.UserId,
                Action = movementDto.Action,
                QuantityChanged = movementDto.QuantityChanged,
                Notes = movementDto.Notes,
                Timestamp = DateTime.UtcNow
            };

            _context.InventoryMovements.Add(movement);
            await _context.SaveChangesAsync();

            return new InventoryMovementDto
            {
                MovementId = movement.MovementId,
                ItemId = movement.ItemId,
                UserId = movement.UserId,
                Action = movement.Action,
                QuantityChanged = movement.QuantityChanged,
                Notes = movement.Notes,
                Timestamp = movement.Timestamp
            };
        }
    }
}
