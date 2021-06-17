using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.EventBusConsumer
{
    public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<BasketCheckoutConsumer> _logger;

        public BasketCheckoutConsumer(IMediator mediator, IMapper mapper, ILogger<BasketCheckoutConsumer> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            var command = _mapper.Map<CheckoutOrderCommand>(context.Message);

            var result = await _mediator.Send(command);

            _logger.LogInformation("Basket checkout event consumed successfully. The order {orderId} has been created.", result);
        }
    }
}
