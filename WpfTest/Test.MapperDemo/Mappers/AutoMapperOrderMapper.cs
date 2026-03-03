using AutoMapper;
using Test.MapperlyDemo.Dtos;
using Test.MapperlyDemo.Models;

namespace Test.MapperlyDemo.Mappers;

/// <summary>
/// AutoMapper 实现：运行时反射映射，配置集中在 <see cref="OrderMapperProfile"/>
/// </summary>
public class AutoMapperOrderMapper : IOrderMapper
{
    private static readonly IMapper _mapper = new MapperConfiguration(cfg =>
        cfg.AddProfile<OrderMapperProfile>()).CreateMapper();

    public OrderDto ToDto(Order order) => _mapper.Map<OrderDto>(order);
}
