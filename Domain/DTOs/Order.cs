namespace DotNetApi.Domain.DTOs;

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = default!;
    public string OrderNumber { get; set; } = default!;
    public string Status { get; set; } = default!;
    public decimal Total { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime CompletedAt { get; set; }
}
