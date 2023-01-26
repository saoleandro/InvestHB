using AutoMapper;
using InvestHB.Domain.Commands;
using InvestHB.Domain.Models;

namespace InvestHB.Domain.AutoMapper
{
    public class OrderMapperProfile : Profile
    {
        public OrderMapperProfile()
        {
            CreateMap<OrderRequest, CreateOrderCommand>();
            CreateMap<DeleteOrderRequest, DeleteOrderCommand>();
            CreateMap<OrderUpdateRequest, UpdateOrderCommand>();
        }
    }
}
