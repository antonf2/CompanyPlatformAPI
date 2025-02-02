using CompanyAPI.DTOs;

namespace CompanyAPI.Services.Interfaces
{
    public interface IInventoryMovementService
    {
        Task<IEnumerable<InventoryMovementDto>> GetAllMovementsAsync();
        Task<InventoryMovementDto> GetMovementByIdAsync(int id);
        Task<InventoryMovementDto> LogMovementAsync(CreateInventoryMovementDto movementDto);
    }
}
