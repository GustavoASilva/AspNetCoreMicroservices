using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries.GetOrders
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, List<GetOrdersDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrdersQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<List<GetOrdersDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetOrdersByUserName(request.UserName);
            return _mapper.Map<List<GetOrdersDto>>(orders);
        }
    }
}