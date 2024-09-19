namespace BusinessObjects.Entities;

public class Order
{
    public required string OrderId { get; set; }
    public string? OrderName { get; set; }
    public int? Quantity { get; set; }
    
}