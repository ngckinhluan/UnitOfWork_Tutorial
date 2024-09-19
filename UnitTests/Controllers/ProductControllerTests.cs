using API.Controllers;
using BusinessObjects.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Services.Interfaces;

namespace UnitTests.Controllers;

public class ProductControllerTests
{
    private readonly Mock<IProductService> _mockProductService;
    private readonly ProductController _controller;

    public ProductControllerTests()
    {
        _mockProductService = new Mock<IProductService>();
        _controller = new ProductController(_mockProductService.Object);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsOk_WithListOfProducts()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { ProductId = 1, ProductName = "Product A"},
            new Product { ProductId = 2, ProductName = "Product B"}
        };
        _mockProductService.Setup(service => service.GetAllAsync()).ReturnsAsync(products);
        var result = await _controller.GetAllAsync();
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnProducts = Assert.IsType<List<Product>>(okResult.Value);
        Assert.Equal(2, returnProducts.Count);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsOk_WithProduct()
    {
        var product = new Product { ProductId = 1, ProductName = "Product A"};
        _mockProductService.Setup(service => service.GetByIdAsync(1)).ReturnsAsync(product);
        var result = await _controller.GetByIdAsync(1);
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnProduct = Assert.IsType<Product>(okResult.Value);
        Assert.Equal(1, returnProduct.ProductId);
        Assert.Equal("Product A", returnProduct.ProductName);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNotFound_WhenProductDoesNotExist()
    {
        _mockProductService.Setup(service => service.GetByIdAsync(999)).ReturnsAsync((Product)null);
        var result = await _controller.GetByIdAsync(999);
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Create_ReturnsCreatedAtAction_WhenProductIsCreated()
    {
        var newProduct = new Product { ProductId = 3, ProductName = "Product C"};
        _mockProductService.Setup(service => service.CreateAsync(newProduct)).Returns(Task.CompletedTask);
        var result = await _controller.Create(newProduct);
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnProduct = Assert.IsType<Product>(createdAtActionResult.Value);
        Assert.Equal(3, returnProduct.ProductId);
        Assert.Equal("Product C", returnProduct.ProductName);
    }

    [Fact]
    public async Task Create_ReturnsBadRequest_WhenProductIsNull()
    {
        var result = await _controller.Create(null);
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Product cannot be null", badRequestResult.Value);
    }

    [Fact]
    public async Task Delete_ReturnsNoContent_WhenProductIsDeleted()
    {
        var product = new Product { ProductId = 1, ProductName = "Product A" };
        _mockProductService.Setup(service => service.GetByIdAsync(1)).ReturnsAsync(product);
        _mockProductService.Setup(service => service.DeleteAsync(1)).Returns(Task.CompletedTask);
        var result = await _controller.DeleteAsync(1);
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_WhenProductDoesNotExist()
    {
        _mockProductService.Setup(service => service.GetByIdAsync(999)).ReturnsAsync((Product)null); 
        var result = await _controller.DeleteAsync(999);
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Update_ReturnsNoContent_WhenProductIsUpdated()
    {
        var existingProduct = new Product { ProductId = 1, ProductName = "Product A" };
        var updatedProduct = new Product { ProductId = 1, ProductName = "Updated Product A" };
        _mockProductService.Setup(service => service.GetByIdAsync(1)).ReturnsAsync(existingProduct);
        _mockProductService.Setup(service => service.UpdateAsync(updatedProduct)).Returns(Task.CompletedTask);
        var result = await _controller.Update(1, updatedProduct);
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Update_ReturnsNotFound_WhenProductDoesNotExist()
    {
        var updatedProduct = new Product { ProductId = 999, ProductName = "Non-Existent Product" };
        _mockProductService.Setup(service => service.GetByIdAsync(999)).ReturnsAsync((Product)null);
        var result = await _controller.Update(999, updatedProduct);
        Assert.IsType<NotFoundResult>(result);
    }
}