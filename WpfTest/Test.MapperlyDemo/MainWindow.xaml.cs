using System.Windows;
using Test.MapperlyDemo.Mappers;
using Test.MapperlyDemo.Models;

namespace Test.MapperlyDemo;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        ShowMappingResult();
    }

    private void ShowMappingResult()
    {
        var order = new Order
        {
            Id = 1001,
            OrderNo = "ORD-20240601-001",
            CreatedAt = new DateTime(2024, 6, 1, 10, 30, 0),
            Status = OrderStatus.Paid,
            Customer = new Customer
            {
                Id = 1,
                FirstName = "三",
                LastName = "张",
                Email = "zhangsan@example.com",
                ShippingAddress = new Address
                {
                    Street = "中关村大街1号",
                    City = "北京",
                    ZipCode = "100080"
                }
            },
            Items =
            [
                new OrderItem { ProductId = 101, ProductName = "机械键盘", Quantity = 1, UnitPrice = 399.00m },
                new OrderItem { ProductId = 102, ProductName = "无线鼠标", Quantity = 2, UnitPrice = 129.00m },
                new OrderItem { ProductId = 103, ProductName = "显示器支架", Quantity = 1, UnitPrice = 259.00m },
            ]
        };

        //var mapper = new OrderMapper();
        var dto = OrderMapper.ToDto(order);

        TxtOrderInfo.Text = $"""
            订单号：{dto.OrderNo}
            状态：{dto.Status}
            创建时间：{dto.CreatedAt}
            客户：{dto.CustomerName}（{dto.CustomerEmail}）
            收货地址：{dto.ShippingAddress.City} {dto.ShippingAddress.Street}（{dto.ShippingAddress.ZipCode}）
            订单总额：¥{dto.TotalAmount:F2}
            """;

        GridItems.ItemsSource = dto.Items;
    }
}
