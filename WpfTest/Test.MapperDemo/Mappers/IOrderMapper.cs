using Test.MapperlyDemo.Dtos;
using Test.MapperlyDemo.Models;

namespace Test.MapperlyDemo.Mappers;

public interface IOrderMapper
{
    OrderDto ToDto(Order order);
}
