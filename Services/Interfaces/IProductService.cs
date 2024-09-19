using BusinessObjects.Entities;

namespace Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<Product>?>? GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task CreateAsync(Product entity);
    Task UpdateAsync(Product entity);
    Task DeleteAsync(int id);
}