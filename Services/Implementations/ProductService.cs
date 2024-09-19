using BusinessObjects.Entities;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implementations;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;


    public async Task<IEnumerable<Product>?>? GetAllAsync() => await _unitOfWork.ProductRepository.GetAllAsync();
    
    public async Task<Product?> GetByIdAsync(int id) => await _unitOfWork.ProductRepository.GetByIdAsync(id);

    public async Task CreateAsync(Product entity) => await _unitOfWork.ProductRepository.AddAsync(entity);

    public async Task UpdateAsync(Product entity) => await _unitOfWork.ProductRepository.UpdateAsync(entity);

    public async Task DeleteAsync(int id) => await _unitOfWork.ProductRepository.DeleteAsync(id);

}