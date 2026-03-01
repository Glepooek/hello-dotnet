namespace Test.MapperlyDemo.Dtos;

public class OrderDto
{
    public int Id { get; set; }
    public string OrderNo { get; set; } = string.Empty;

    /// <summary>
    /// 格式化后的创建时间（源字段 CreatedAt → 字符串）
    /// </summary>
    public string CreatedAt { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// 扁平化：Customer.FirstName + Customer.LastName → CustomerName
    /// </summary>
    public string CustomerName { get; set; } = string.Empty;

    public string CustomerEmail { get; set; } = string.Empty;

    /// <summary>
    /// 嵌套对象映射
    /// </summary>
    public AddressDto ShippingAddress { get; set; } = null!;

    /// <summary>
    /// 集合映射
    /// </summary>
    public List<OrderItemDto> Items { get; set; } = [];

    /// <summary>
    /// 计算属性：所有 Item 的总金额
    /// </summary>
    public decimal TotalAmount { get; set; }
}

public class AddressDto
{
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
}

public class OrderItemDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// 计算属性：Quantity * UnitPrice
    /// </summary>
    public decimal SubTotal { get; set; }
}
