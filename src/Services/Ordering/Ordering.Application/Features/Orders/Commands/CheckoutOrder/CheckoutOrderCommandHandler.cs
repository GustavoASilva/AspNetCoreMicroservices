using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models;
using Ordering.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<CheckoutOrderCommandHandler> _logger;

        public CheckoutOrderCommandHandler(IOrderRepository orderRepository,
                                           IMapper mapper,
                                           IEmailService emailService,
                                           ILogger<CheckoutOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = _mapper.Map<Order>(request);

            var addedOrder = await _orderRepository.AddAsync(orderEntity);

            _logger.LogInformation($"Order {addedOrder.Id} created successfully.");



            await SendEmailAsync(addedOrder);
            return addedOrder.Id;
        }

        private async Task SendEmailAsync(Order order)
        {
            var email = new Email()
            {
                To = order.EmailAddress,
                Subject = $"New Order {order.Id}",
                Body = "Order was created."
            };

            try
            {
                await _emailService.SendEmailAsync(email);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error sending email for order {order.Id} with the mail service: {e.Message}");
            }
        }
    }
}
