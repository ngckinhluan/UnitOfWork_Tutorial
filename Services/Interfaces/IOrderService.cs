using BusinessObjects.Entities;

namespace Services.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<Order>?>? GetAllAsync();
    Task<Order?> GetByIdAsync(int id);
    Task CreateAsync(Order entity);
    Task UpdateAsync(Order entity);
    Task DeleteAsync(int id);
}