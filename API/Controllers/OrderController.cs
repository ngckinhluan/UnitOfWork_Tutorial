using BusinessObjects.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        
        public OrderController(IOrderService orderService) => _orderService = orderService;
        
        [HttpGet]
        public async Task<IActionResult> GetAllAsync() 
        {
            var orders = await _orderService.GetAllAsync();
            return Ok(orders);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Order entity)
        {
            if (entity == null)
            {
                return BadRequest("Order cannot be null");
            }
            await _orderService.CreateAsync(entity);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = entity.OrderId }, entity);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Order entity)
        {
            var existingEntity = await _orderService.GetByIdAsync(id);
            if (existingEntity == null)
            {
                return NotFound();
            }
            entity.OrderId = id; 
            await _orderService.UpdateAsync(entity);
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            await _orderService.DeleteAsync(id);
            return NoContent();
        }
    }
}