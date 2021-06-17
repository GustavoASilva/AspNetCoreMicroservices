using AutoMapper;
using EventBus.Messages.Events;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;

namespace Ordering.API.Mappings
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<BasketCheckoutEvent, CheckoutOrderCommand>().ReverseMap();
        }
    }
}
