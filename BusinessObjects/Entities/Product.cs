namespace BusinessObjects.Entities;

public class Product
{
    public required string ProductId { get; set; }
    public string? ProductName { get; set; }
    public int? Quantity { get; set; }
}