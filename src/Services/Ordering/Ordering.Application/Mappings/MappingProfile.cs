using AutoMapper;
using EventBus.Messages.Events;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using Ordering.Application.Features.Orders.Commands.UpdateOrder;
using Ordering.Application.Features.Orders.Queries.GetOrders;
using Ordering.Domain.Entities;

namespace Ordering.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, GetOrdersDto>().ReverseMap();
            CreateMap<Order, CheckoutOrderCommand>().ReverseMap();
            CreateMap<Order, UpdateOrderCommand>().ReverseMap();
        }
    }
}
