using BusinessObjects.Entities;

namespace Repositories.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<Product> ProductRepository { get; }
    IRepository<Order> OrderRepository { get; }
    Task SaveChangeAsync();
}