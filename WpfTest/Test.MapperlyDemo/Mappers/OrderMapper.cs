using Riok.Mapperly.Abstractions;
using Test.MapperlyDemo.Dtos;
using Test.MapperlyDemo.Models;

namespace Test.MapperlyDemo.Mappers;

[Mapper]
public partial class OrderMapper
{
    public static OrderDto ToDto(Order order)
    {
        var dto = ToOrderDto(order);
        // 手动处理计算属性
        dto.CustomerName = $"{order.Customer.FirstName} {order.Customer.LastName}";
        dto.TotalAmount = order.Items.Sum(i => i.Quantity * i.UnitPrice);
        foreach (var (item, itemDto) in order.Items.Zip(dto.Items))
        {
            itemDto.SubTotal = item.Quantity * item.UnitPrice;
        }

        return dto;
    }

    /// <summary>
    /// 字段重命名：Customer.Email → CustomerEmail，Customer.ShippingAddress → ShippingAddress
    /// 枚举映射：OrderStatus → string
    /// </summary>
    [MapProperty(nameof(Order.Customer) + "." + nameof(Models.Customer.Email), nameof(OrderDto.CustomerEmail))]
    [MapProperty(nameof(Order.Customer) + "." + nameof(Models.Customer.ShippingAddress), nameof(OrderDto.ShippingAddress))]
    [MapProperty(nameof(Order.CreatedAt), nameof(OrderDto.CreatedAt), StringFormat = "yyyy-MM-dd HH:mm:ss")]
    private static partial OrderDto ToOrderDto(Order order);

    // 以下两个方法由 Mapperly 在编译时自动生成实现，供集合映射和嵌套对象映射内部调用
    private static partial OrderItemDto ToItemDto(OrderItem item);
    private static partial AddressDto ToAddressDto(Address address);
    private static string MapStatus(OrderStatus status) => status.ToString();
}
