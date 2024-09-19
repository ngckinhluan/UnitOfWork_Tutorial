using API.Controllers;
using BusinessObjects.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Moq;

namespace UnitTests.Controllers;

public class OrderControllerTests
{
    private readonly Mock<IOrderService> _mockOrderService;
    private readonly OrderController _controller;

    public OrderControllerTests()
    {
        _mockOrderService = new Mock<IOrderService>();
        _controller = new OrderController(_mockOrderService.Object);
    }
    
    [Fact]
    public async Task GetAllAsync_ReturnsOk_WithListOfOrders()
    {
        var orders = new List<Order>
        {
            new Order { OrderId = 1, OrderName = "John Doe" },
            new Order { OrderId = 2, OrderName = "Jane Smith"}
        };
        _mockOrderService.Setup(service => service.GetAllAsync()).ReturnsAsync(orders);
        var result = await _controller.GetAllAsync();
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnOrders = Assert.IsType<List<Order>>(okResult.Value);
        Assert.Equal(2, returnOrders.Count);
    }
    
    [Fact]
    public async Task GetByIdAsync_ReturnsOk_WithOrder()
    {
        var order = new Order { OrderId = 1, OrderName = "Cong Phuong Pha Ca Phe :)))"};
        _mockOrderService.Setup(service => service.GetByIdAsync(1)).ReturnsAsync(order);
        var result = await _controller.GetByIdAsync(1);
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnOrder = Assert.IsType<Order>(okResult.Value);
        Assert.Equal(1, returnOrder.OrderId);
        Assert.Equal("Cong Phuong Pha Ca Phe :)))", returnOrder.OrderName);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNotFound_WhenOrderDoesNotExist()
    {
        _mockOrderService.Setup(service => service.GetByIdAsync(999)).ReturnsAsync((Order)null);
        var result = await _controller.GetByIdAsync(999);
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Create_ReturnsCreatedAtAction_WhenOrderIsCreated()
    {
        var newOrder = new Order { OrderId = 3 };
        _mockOrderService.Setup(service => service.CreateAsync(newOrder)).Returns(Task.CompletedTask);
        var result = await _controller.Create(newOrder);
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnOrder = Assert.IsType<Order>(createdAtActionResult.Value);
        Assert.Equal(3, returnOrder.OrderId);
    }

    [Fact]
    public async Task Create_ReturnsBadRequest_WhenOrderIsNull()
    {
        var result = await _controller.Create(null);
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Order cannot be null", badRequestResult.Value);
    }

    [Fact]
    public async Task Delete_ReturnsNoContent_WhenOrderIsDeleted()
    {
       
        var order = new Order { OrderId = 1};
        _mockOrderService.Setup(service => service.GetByIdAsync(1)).ReturnsAsync(order);
        _mockOrderService.Setup(service => service.DeleteAsync(1)).Returns(Task.CompletedTask);
        var result = await _controller.DeleteAsync(1);
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_WhenOrderDoesNotExist()
    {
        _mockOrderService.Setup(service => service.GetByIdAsync(999)).ReturnsAsync((Order)null); 
        var result = await _controller.DeleteAsync(999);
        Assert.IsType<NotFoundResult>(result);
    }

}