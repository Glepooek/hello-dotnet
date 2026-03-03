using AutoMapper;
using Test.MapperlyDemo.Dtos;
using Test.MapperlyDemo.Models;

namespace Test.MapperlyDemo.Mappers;

/// <summary>
/// AutoMapper 映射配置：
/// 字段重命名、扁平化、枚举转字符串、计算属性均在此声明
/// </summary>
public class OrderMapperProfile : Profile
{
    public OrderMapperProfile()
    {
        CreateMap<Address, AddressDto>();

        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(dest => dest.SubTotal, opt => opt.MapFrom(src => src.Quantity * src.UnitPrice));

        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss")))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => $"{src.Customer.FirstName} {src.Customer.LastName}"))
            .ForMember(dest => dest.CustomerEmail, opt => opt.MapFrom(src => src.Customer.Email))
            .ForMember(dest => dest.ShippingAddress, opt => opt.MapFrom(src => src.Customer.ShippingAddress))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.Items.Sum(i => i.Quantity * i.UnitPrice)));
    }
}
