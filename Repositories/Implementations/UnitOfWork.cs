using BusinessObjects.Context;
using BusinessObjects.Entities;
using Repositories.Interfaces;

namespace Repositories.Implementations;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        ProductRepository = new Repository<Product>(context);
        OrderRepository = new Repository<Order>(context);
    }

    public IRepository<Product> ProductRepository { get; private set; }
    public IRepository<Order> OrderRepository { get; private set; }
    
    public async Task SaveChangeAsync() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
    
    
}
    