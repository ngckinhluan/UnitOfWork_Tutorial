using BusinessObjects.Entities;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implementations;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    
    public OrderService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<IEnumerable<Order>?>? GetAllAsync() => await _unitOfWork.OrderRepository.GetAllAsync();

    public async Task<Order?> GetByIdAsync(int id) => await _unitOfWork.OrderRepository.GetByIdAsync(id);

    public async Task CreateAsync(Order entity) => await _unitOfWork.OrderRepository.AddAsync(entity);

    public async Task UpdateAsync(Order entity) => await _unitOfWork.OrderRepository.UpdateAsync(entity);

    public async Task DeleteAsync(int id) => await _unitOfWork.OrderRepository.DeleteAsync(id);

}