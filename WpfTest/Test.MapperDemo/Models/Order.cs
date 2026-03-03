// 领域模型（Domain Model），反映业务真实结构，不关心展示细节
namespace Test.MapperlyDemo.Models;

public class Order
{
    public int Id { get; set; }
    public string OrderNo { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public OrderStatus Status { get; set; }
    public Customer Customer { get; set; } = null!;
    public List<OrderItem> Items { get; set; } = [];
}

public class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Address ShippingAddress { get; set; } = null!;
}

public class Address
{
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
}

public class OrderItem
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}

public enum OrderStatus
{
    Pending,
    Paid,
    Shipped,
    Completed,
    Cancelled
}