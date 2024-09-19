using BusinessObjects.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace API.Controllers;
[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    
    public ProductController(IProductService productService) => _productService = productService;
    
    
    [HttpGet]
    public async Task<IActionResult> GetAllAsync() 
    {
        var products = await _productService.GetAllAsync();
        return Ok(products);
    }
    
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }
    
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Product entity)
    {
        if (entity == null)
        {
            return BadRequest("Product cannot be null");
        }
        await _productService.CreateAsync(entity);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = entity.ProductId }, entity);
    }
    
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        await _productService.DeleteAsync(id);
        return NoContent();
    }
  
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Product entity)
    {
        var existingEntity = await _productService.GetByIdAsync(id);
        if (existingEntity == null)
        {
            return NotFound();
        }
        entity.ProductId = id; 
        await _productService.UpdateAsync(entity);
        return NoContent();
    }
    
    
}